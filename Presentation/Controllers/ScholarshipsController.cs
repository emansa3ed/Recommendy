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
        public async Task<ActionResult<IEnumerable<EditedScholarshipDto>>> GetScholarshipsForUniversity([FromRoute] string universityId)
        {
            try
            {
                var scholarships = await _service.ScholarshipService
                    .GetAllScholarshipsForUniversity(universityId, trackChanges: false);
                if (!scholarships.Any())
                    return NotFound($"No scholarships found for university with ID: {universityId}");
                return Ok(scholarships);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error while retrieving scholarships");
            }
        }


        [HttpPost]
        public async Task<ActionResult<EditedScholarshipDto>> CreateScholarshipForUniversity(
        [FromRoute] string universityId,
        [FromForm] ScholarshipForCreationDto scholarshipForCreation)
        {
            try
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

                return Created($"/api/Scholarships/Scholarship/{createdScholarship.Id}", createdScholarship);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating scholarship for university {UniversityId}", universityId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpGet("edit/{id:int}")]
        public async Task<ActionResult<EditScholarshipProfileDto>> GetScholarshipForEdit(string universityId, int id)
        {
            try
            {

                var scholarship = await _service.ScholarshipService.GetScholarshipById(id, trackChanges: false);
                if (scholarship == null)
                    return NotFound($"Scholarship with ID {id} not found.");


                var fundedOptions = Enum.GetValues(typeof(Funded))
                    .Cast<Funded>()
                    .Select(e => new EnumDto((int)e, e.ToString()))
                    .ToList();

                var degreeOptions = Enum.GetValues(typeof(Degree))
                    .Cast<Degree>()
                    .Select(e => new EnumDto((int)e, e.ToString()))
                    .ToList();


                var response = new EditScholarshipProfileDto
                {
                    Scholarship = scholarship,
                    FundedOptions = fundedOptions,
                    DegreeOptions = degreeOptions
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while retrieving scholarship for edit.");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateScholarshipForUniversity(string universityId, int id, [FromForm] ScholarshipDto scholarshipDto)
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

            // Only allowed for university 
            if (!user.Discriminator.Equals("University", StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }

            await _service.ScholarshipService.UpdateScholarshipForUniversityAsync(universityId, id, scholarshipDto, trackChanges: true);
            return NoContent();
        }





           [HttpDelete("{id:int}")]
   public async Task<IActionResult> DeleteScholarshipForUniversity(string universityId, int id)
   {
       // var username = User.Identity.Name;
       // if (username == null)
       //{
       //     return Unauthorized(); 
       //   }   
       // var user = await _service.UserService.GetDetailsByUserName(username);
       // if (user == null)
       // {
       //   return NotFound();
       //}
       // only allowed for university and admin
       // if (!user.Discriminator.Equals("University", StringComparison.OrdinalIgnoreCase) &&
       // / //!user.Discriminator.Equals("Admin", StringComparison.OrdinalIgnoreCase))
       // {
       // return Forbid();
       //}
       try
       {
           await _service.ScholarshipService.DeleteScholarshipForUniversityAsync(universityId, id, trackChanges: false);
           return NoContent();
       }
       catch (UniversityNotFoundException ex)
       {
           return NotFound(ex.Message);

       }

       catch (ScholarshipNotFoundException ex)
       {
           return NotFound(ex.Message);


       }
   }
    }
}
