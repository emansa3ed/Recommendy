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
            var user = _userManager.FindByEmailAsync(userForRegistration.Email);

            return Ok($"Registration successful. Please check your email to confirm your account. userId{user.Id}");

        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] UserForLoginDto user)
        {
            if (!await _service.AuthenticationService.ValidateUser(user))
                return Unauthorized("Invalid username or password.");

          var user1  =  await _service.UserService.GetDetailsByUserName(user.UserName);
       
            if (!user1.EmailConfirmed)
                   return BadRequest($"User is not Confirm his email and his Id {user1.Id}");
       

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
        
    }
}
