using Entities.Exceptions;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public  class UserController : ControllerBase
    {
        private readonly IServiceManager _service;
        public UserController(IServiceManager service) 
        { 
            _service = service;
        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto == null)
            {
                return BadRequest("ChangePasswordDto object is null");
            }
            var username = User.Identity.Name;
            if (username == null)
            {
                throw new UnauthorizedAccessException();//////استخدام middel ware
            }

            var user = await _service.UserService.GetDetailsByUserName(username);
            if (user == null)
            {
                return NotFound("User not found.");
            }
           

            try
            {
                await _service.UserService.ChangePasswordAsync(user.Id, changePasswordDto);
                return NoContent();
            }
            catch (UniversityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("upload-profile-picture")]

        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            var username = User.Identity.Name;
            if (username == null)
            {
                return Unauthorized();
            }

            var user = await _service.UserService.GetDetailsByUserName(username);
            if (user == null)
            {
                return NotFound("User not found.");
            }
           
            try
            {
                var imageUrl = await _service.UserService.UploadProfilePictureAsync(file, user.Id);
                return Ok(new { ImageUrl = imageUrl });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
