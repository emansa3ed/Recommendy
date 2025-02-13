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
    [Route("api/universities")]
    [ApiController]
    [Authorize(Roles ="University")]
    public class UniversitiesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public UniversitiesController(IServiceManager service) => _service = service;
        [HttpGet("profile/{id}")]
        public IActionResult GetUniversity(string id)
        {
            try
            {
                var university = _service.UniversityService.GetUniversity(id, trackChanges: false);
                return Ok(university);
            }
            catch (UniversityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPatch("edit-profile/{id}")]

        public async Task<IActionResult> EditProfile(string id, [FromBody] UniversityDto universityDto)
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
            if (!user.Discriminator.Equals("University", StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }
            try
            {
                await _service.UniversityService.UpdateUniversity(id, universityDto, trackChanges: true);
                return NoContent();
            }
            catch (UniversityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (CountryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("edit-profile/{id}")]
        public IActionResult GetEditProfile(string id)
        {
            try
            {
                var university = _service.UniversityService.GetUniversity(id, trackChanges: false);


                var countries = _service.CountryService.GetAllCountries(trackChanges: false);


                var response = new EditUniversityProfileDto
                {
                    UniversityUrl = university.UniversityUrl,
                    UserName = university.UserName,
                    CountryId = university.CountryId,
                    Bio = university.Bio,
                    PhoneNumber = university.PhoneNumber,
                    Countries = countries
                };

                return Ok(response);
            }
            catch (UniversityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        
        

       

    }
}
