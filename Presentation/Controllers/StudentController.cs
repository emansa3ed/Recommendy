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
    [Route("api/students")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {

        private readonly IServiceManager _service;
        public StudentController(IServiceManager ServiceManager)
        {

            _service = ServiceManager;

        }

   

        [HttpGet("profile/{id}")]
        public IActionResult GetStudent(string id)
        {
            try
            {
                var student = _service.StudentService.GetStudent(id, trackChanges: false);
                return Ok(student);
            }
            catch (StudentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPatch("edit-profile/{id}")]
        public async Task<IActionResult> UpdateStudentProfile(string id, [FromBody] StudentForUpdateDto studentForUpdate)
        {
            if (studentForUpdate == null)
            {
                return BadRequest("StudentForUpdateDto object is null");
            }
            var username = User.Identity.Name;
            if (username == null)
            {
                return Unauthorized();
            }

            try
            {
                var user = await _service.UserService.GetDetailsByUserName(username);
                if (user == null)
                {
                    return NotFound();
                }
                //only allowed for student 
                if (!user.Discriminator.Equals("Student", StringComparison.OrdinalIgnoreCase))
                {
                    return Forbid();
                }
                await _service.StudentService.UpdateStudentProfileAsync(id, studentForUpdate);
                return NoContent();
            }
            catch (StudentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
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
                return Unauthorized();
            }

            var user = await _service.UserService.GetDetailsByUserName(username);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            // only allowed for student 
            if (!user.Discriminator.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }

            try
            {
                await _service.UserService.ChangePasswordAsync(user.Id, changePasswordDto);
                return NoContent();
            }
            catch (StudentNotFoundException ex)
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
            // only allowed for student 
            if (!user.Discriminator.Equals("Students", StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }
            try
            {
                var imageUrl = await _service.StudentService.UploadProfilePictureAsync(file, user.Id);
                return Ok(new { ImageUrl = imageUrl });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("saved-scholarships")]
        public async Task<IActionResult> GetSavedScholarships()
        {
            try
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


                if (!user.Discriminator.Equals("Student", StringComparison.OrdinalIgnoreCase))
                {
                    return Forbid();
                }


                var savedScholarships = await _service.OpportunityService.GetSavedScholarshipsAsync(user.Id);

                return Ok(savedScholarships);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while retrieving saved scholarships.");
            }
        }

        [HttpGet("saved-internships")]
        public async Task<IActionResult> GetSavedInternships()
        {
            try
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


                if (!user.Discriminator.Equals("Student", StringComparison.OrdinalIgnoreCase))
                {
                    return Forbid();
                }


                var savedInternships = await _service.OpportunityService.GetSavedInternshipsAsync(user.Id);

                return Ok(savedInternships);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while retrieving saved internships.");
            }
        }

    }
}
