using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers

{
    [Route("api/universities")]
    [ApiController]
    //[Authorize]
    public class UniversitiesController: ControllerBase
    {
        private readonly IServiceManager _service;
        public UniversitiesController(IServiceManager service) => _service = service;
        [HttpGet("profile/{id}")]
        public IActionResult GetUniversity(string id)
        {
            var university = _service.UniversityService.GetUniversity(id, trackChanges: false);
            return Ok(university);
        }


        [HttpPatch("edit-profile/{id}")]
       
        public async Task<IActionResult> EditProfile(string id, [FromBody] UniversityDto universityDto)
        {
            var username = User.Identity.Name;
           // if (username == null)
           // {
           //     return Unauthorized();
           // }
          //  var user = await _service.UserService.GetDetailsByUserName(username);
          //  if (user == null)
          //  {
          //      return NotFound();
          //  }
            // only allowed for university 
           // if (!user.Discriminator.Equals("University", StringComparison.OrdinalIgnoreCase))
        //    {
                //return Forbid();
          //  }
            await _service.UniversityService.UpdateUniversity(id, universityDto, trackChanges: true);
            return NoContent();
        }


        [HttpPost("upload-profile-picture")]
      
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            var username = User.Identity.Name;
           // if (username == null)
          //  {
              //  return Unauthorized();
          //  }

            var user = await _service.UserService.GetDetailsByUserName(username);
           // if (user == null)
         //   {
              //  return NotFound("User not found.");
         //   }
            // only allowed for university 
           // if (!user.Discriminator.Equals("University", StringComparison.OrdinalIgnoreCase))
           // {
              //  return Forbid();
           // }
            try
            {
                var imageUrl = await _service.UniversityService.UploadProfilePictureAsync(file, user.Id);
                return Ok(new { ImageUrl = imageUrl });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
