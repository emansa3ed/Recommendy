using Entities.Exceptions;
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
    [Route("api/companies")]
    [ApiController]
   // [Authorize]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CompaniesController(IServiceManager service) => _service = service;


        [HttpGet("profile/{id}")]
        public IActionResult GetCompany(string id)
        {
            try
            {
                var company = _service.CompanyService.GetCompany(id, trackChanges: false);
                return Ok(company);
            }
            catch (CompanyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpPatch("edit-profile/{id}")]

        public async Task<IActionResult> EditProfile(string id, [FromBody] CompanyDto companyDto)
        {
            var username = User.Identity.Name;
            if (username == null)
            {
                return Unauthorized();
            }
            var user = await _service.UserService.GetDetailsByUserName(username);
            if (user == null)
            {
                return NotFound();
            }
            // only allowed for university 
            if (!user.Discriminator.Equals("Company", StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }
            await _service.CompanyService.UpdateCompany(id, companyDto, trackChanges: true);
            return NoContent();
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
            // only allowed for company 
            if (!user.Discriminator.Equals("Company", StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
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
            // only allowed for university 
            if (!user.Discriminator.Equals("Company", StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }
            try
            {
                var imageUrl = await _service.CompanyService.UploadProfilePictureAsync(file, user.Id);
                return Ok(new { ImageUrl = imageUrl });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
