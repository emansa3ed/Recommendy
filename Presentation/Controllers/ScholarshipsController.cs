using Microsoft.AspNetCore.Authorization;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.Contracts;
using Shared.DTO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Entities.Exceptions;
using Entities.GeneralResponse;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/universities/{universityId}/scholarships")]
    [ApiController]
    [Authorize(Roles ="University")]
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
        public async Task<ActionResult<ApiResponse<IEnumerable<EditedScholarshipDto>>>> GetScholarshipsForUniversity([FromRoute] string universityId)
        {
            var scholarships = await _service.ScholarshipService
                .GetAllScholarshipsForUniversity(universityId, trackChanges: false);
            if (!scholarships.Any())
                return Ok(new ApiResponse<IEnumerable<EditedScholarshipDto>> { Success=true,Data=null,Message= $"No scholarships found for university with ID: {universityId}" });
			return Ok(new ApiResponse<IEnumerable<EditedScholarshipDto>> { Success = true, Data = scholarships});
		}


		[HttpPost]
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

            // Log the file information for debugging
            if (scholarshipForCreation.ImageFile != null)
            {
                _logger.LogInformation($"Received file: {scholarshipForCreation.ImageFile.FileName}, " +
                                        $"Length: {scholarshipForCreation.ImageFile.Length}");
            }

            _logger.LogInformation($"Requirements: {JsonSerializer.Serialize(scholarshipForCreation.Requirements)}");



            var createdScholarship = await _service.ScholarshipService
                .CreateScholarshipForUniversity(universityId, scholarshipForCreation, trackChanges: false);

            _logger.LogInformation($"Successfully created scholarship with ID: {createdScholarship.Id}");

            return Created($"/api/Scholarships/Scholarship/{createdScholarship.Id}",new ApiResponse<EditedScholarshipDto> { Success = true, Data = createdScholarship});
        }

        
        [HttpGet("{id:int}")]
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
        public async Task<IActionResult> UpdateScholarshipForUniversity(string universityId, int id, [FromForm] ScholarshipDto scholarshipDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _service.ScholarshipService.UpdateScholarshipForUniversityAsync(universityId, id, scholarshipDto, trackChanges: true);
            return NoContent();
        }





       [HttpDelete("{id:int}")]
       public async Task<IActionResult> DeleteScholarshipForUniversity(string universityId, int id)
       {
            await _service.ScholarshipService.DeleteScholarshipForUniversityAsync(universityId, id, trackChanges: false);
            return NoContent();
       }
    }
}
