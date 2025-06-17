using Microsoft.AspNetCore.Authorization;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.Contracts;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Entities.Exceptions;
using Entities.GeneralResponse;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Shared.RequestFeatures;
using Shared.DTO.Scholaship;

namespace Presentation.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/universities/{universityId}/scholarships")]
    [ApiController]
  
    public class ScholarshipsController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly ILogger<ScholarshipsController> _logger;

        public ScholarshipsController(IServiceManager service, ILogger<ScholarshipsController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }



        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ApiResponse<PagedList<EditedScholarshipDto>>>> GetScholarshipsForUniversity([FromRoute] string universityId, [FromQuery] ScholarshipsParameters scholarshipsParameters)
        {
            var scholarships = await _service.ScholarshipService
                .GetAllScholarshipsForUniversity(universityId, scholarshipsParameters, trackChanges: false);
            if (!scholarships.Any())
                return Ok(new ApiResponse<PagedList<EditedScholarshipDto>> { Success=true,Data=null,Message= $"No scholarships found for university with ID: {universityId}" });
			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(scholarships.MetaData));
			return Ok(new ApiResponse<PagedList<EditedScholarshipDto>> { Success = true, Data = scholarships});
		}


		[HttpPost]
        [Authorize(Roles = "University")]
        [Authorize(Policy = "VerifiedOrganization")]
        public async Task<ActionResult<ApiResponse<EditedScholarshipDto>>> CreateScholarshipForUniversity(
        [FromRoute] string universityId,
        [FromForm] ScholarshipForCreationDto scholarshipForCreation)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for scholarship creation");
                return BadRequest(ModelState);
            }

            if (scholarshipForCreation == null)
            {
                _logger.LogWarning("ScholarshipForCreation object sent from client is null");
                return BadRequest("Scholarship data is null.");
            }

            if (scholarshipForCreation.ImageFile != null)
            {
                _logger.LogInformation($"Received file: {scholarshipForCreation.ImageFile.FileName}, " +
                                        $"Length: {scholarshipForCreation.ImageFile.Length}");
            }

            _logger.LogInformation($"Requirements: {JsonSerializer.Serialize(scholarshipForCreation.Requirements)}");

			var UserName = User.Identity?.Name;

			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (user.Id != universityId)
				return BadRequest(new ApiResponse<EditedScholarshipDto> { Success = false, Message = "universityId don't match with authorized one", Data = null });

			var createdScholarship = await _service.ScholarshipService
                .CreateScholarshipForUniversity(universityId, scholarshipForCreation, trackChanges: false);

            _logger.LogInformation($"Successfully created scholarship with ID: {createdScholarship.Id}");

            return Created($"/api/Scholarships/Scholarship/{createdScholarship.Id}",new ApiResponse<EditedScholarshipDto> { Success = true, Data = createdScholarship});
        }

        
        [HttpGet("{id:int}")]
        [Authorize(Roles = "University")]
        public async Task<ActionResult<ApiResponse<GetScholarshipDto>>> GetScholarshipForEdit(string universityId, int id)
        {
            var scholarship = await _service.ScholarshipService.GetScholarshipById(id, trackChanges: false);
            var fundedOptions = Enum.GetValues(typeof(Funded))
                .Cast<Funded>()
                .Select(e => new EnumDto((int)e, e.ToString()))
                .ToList();

            var degreeOptions = Enum.GetValues(typeof(Degree))
                .Cast<Degree>()
                .Select(e => new EnumDto((int)e, e.ToString()))
                .ToList();

            return Ok(new ApiResponse<GetScholarshipDto> { Success=true,Data= scholarship });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "University")]
        [Authorize(Policy = "VerifiedOrganization")]
        public async Task<IActionResult> UpdateScholarshipForUniversity(string universityId, int id, [FromForm] ScholarshipDto scholarshipDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

			var UserName = User.Identity?.Name;

			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (user.Id != universityId)
				return BadRequest(new ApiResponse<EditedScholarshipDto> { Success = false, Message = "universityId don't match with authorized one", Data = null });

			await _service.ScholarshipService.UpdateScholarshipForUniversityAsync(universityId, id, scholarshipDto, trackChanges: true);
            return NoContent();
        }





       [HttpDelete("{id:int}")]
        [Authorize(Roles = "University")]
        [Authorize(Policy = "VerifiedOrganization")]
        public async Task<IActionResult> DeleteScholarshipForUniversity(string universityId, int id)
       {

			var UserName = User.Identity?.Name;

			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (user.Id != universityId)
				return BadRequest(new ApiResponse<EditedScholarshipDto> { Success = false, Message = "universityId don't match with authorized one", Data = null });

			await _service.ScholarshipService.DeleteScholarshipForUniversityAsync(universityId, id, trackChanges: false);
            return NoContent();
       }
    }
}
