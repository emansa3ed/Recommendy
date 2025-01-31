using Entities.Models;
using Microsoft.AspNetCore.Http;
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
        public AuthenticationController(IServiceManager service) => _service = service;



        [HttpPost("Register")]
       
        public async Task<IActionResult> RegisterUser([FromForm] UserForRegistrationDto userForRegistration )
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
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] UserForLoginDto user)
        {
            if (!await _service.AuthenticationService.ValidateUser(user))
                return Unauthorized("Invalid username or password.");
            User user1 = _service.UserService.GetDetails(user.UserName).Result;


            return Ok(new
            {
                Token = await _service.AuthenticationService.CreateToken(),
                user1.Email,
                user1.Discriminator,
                user1.UrlPicture,
                user1.UserName,
                user1.Bio,
                user1.Id,
                user1.PhoneNumber
             
            });
        }
    }
}
