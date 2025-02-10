using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly  UserManager<User> _userManager;
        public AuthenticationController(IServiceManager service, UserManager<User> userManage)
        {
            _service = service;
            _userManager = userManage;

        }



        [HttpPost("Register")]
       
        public async Task<IActionResult> RegisterUser([FromForm] UserForRegistrationDto userForRegistration )
        {
            try
            {
                var result = await
               _service.AuthenticationService.RegisterUser(userForRegistration);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);


                }
            }
            catch (Exception ex) {

                throw new Exception($"Failed. {ex.Message} | Inner Exception: {ex.InnerException?.Message}");

            }
            var user =  await  _userManager.FindByEmailAsync(userForRegistration.Email);

            return StatusCode(201, new ApiResponse<string>
            {
                Message = "Registration successful. Please check your email to confirm your account",
                Success = true,
                Data = user.Id
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] UserForLoginDto user)
        {
            if (!await _service.AuthenticationService.ValidateUser(user))
                return Unauthorized("Invalid username or password.");

          var user1  =  await _service.UserService.GetDetailsByUserName(user.UserName);

            if (!user1.EmailConfirmed)
                return StatusCode(401, new ApiResponse<string>
                {
                    Message = "User is not Confirm his email",
                    Success = false,
                    Data = user1.Id
                });
            


            var tokenDto = await _service.AuthenticationService .CreateToken(populateExp: true);
            return Ok(tokenDto);


        }

        [HttpGet("GetMe")]

        public async Task<IActionResult> GetMe(string token)
        {
            try
            {
                var result =   await  _service.UserService.GetDetailsbyId(_service.AuthenticationService.ExtractUserIdFromToken(token));

                return Ok(result);
            }
            catch (Exception ex) {

                return StatusCode(500, new ApiResponse<int>
                {
                    Success = false,
                    Message = "An error occurred ." + ex.Message,

                });

            }
        }

        [HttpPost("ForgotPassword")]

        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {

            var  user =   _userManager.FindByEmailAsync(forgotPasswordDto.Email).Result;
            if (user == null)
                return BadRequest("Email not found");

           var result=   await  _service.userCodeService.GenerateUserCodeForResetPasswordAsync(user.Id);

            if (result != "Email Sended") return BadRequest("some thing error please try again");

                return Ok(result);



        }

        [HttpPost("ResetPassword")]

        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = _userManager.FindByEmailAsync(resetPasswordDto.Email).Result;
            if (user == null)
                return BadRequest("Email not found");

             var result = await _service.EmailsService.ConfirmationForResetPasswordAsync(user.Id, resetPasswordDto.Code,resetPasswordDto.NewPassword);

            if(result != "Password reset successfully")
                return BadRequest(result);
  

                return Ok("Password reset successfully");
           

        }


    }
}
