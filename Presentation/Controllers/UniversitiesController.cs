using Entities.Exceptions;
using Entities.GeneralResponse;
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
        public async Task<ActionResult<ApiResponse<UniversityViewDto>>> GetUniversity(string id)
        {
            var university = await _service.UniversityService.GetUniversityAsync(id, trackChanges: false);
            return Ok(new ApiResponse<UniversityViewDto> { Success=true,Data=university});
        }


        [HttpPut("edit-profile/{id}")]

        public async Task<IActionResult> EditProfile(string id, [FromBody] UniversityDto universityDto)
        {
            var username = User.Identity.Name;
            var user = await _service.UserService.GetDetailsByUserName(username);
            await _service.UniversityService.UpdateUniversity(id, universityDto, trackChanges: true);
            return NoContent();
        }


        [HttpGet("edit-profile/{id}")]
        public async Task<ActionResult<ApiResponse<EditUniversityProfileDto>>> GetEditProfile(string id)
        {
            var university =await _service.UniversityService.GetUniversityAsync(id, trackChanges: false);


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

            return Ok(new ApiResponse<EditUniversityProfileDto> { Success=true,Data=response});
        }


    }
}
