using Entities.GeneralResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Internship;
using Shared.DTO.opportunity;
using Shared.DTO.Scholaship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class OpportunitiesController : ControllerBase
    {

        private readonly IServiceManager _service;
        public OpportunitiesController(IServiceManager ServiceManager)
        {

            _service = ServiceManager;

        }

        [HttpGet("Scholarships/all")]
        public async Task<IActionResult> GetScholarships()
        {

            var scholarships = await _service.ScholarshipService.GetAllScholarships(trackChanges: false);
            return Ok(scholarships);

        }



        [HttpGet("Scholarships/{id:int}")]
        public async Task<ActionResult<GetScholarshipDto>> GetOneScholarship(int id)
        {

            var scholarship = await _service.ScholarshipService.GetScholarshipById(id, trackChanges: false);

            return Ok(new ApiResponse<GetScholarshipDto>
            {
                Success = true,
                Message = "fetch data  successfully.",
                Data = scholarship
            });

        }



        [HttpGet("Internships/all")]
        public async Task<IActionResult> GetInternships()
        {

            var internships = await _service.InternshipService.GetAllInternships(trackChanges: false);
            return Ok(internships);

        }


        [HttpGet("Internships/{id:int}")]
        public async Task<ActionResult<InternshipDto>> GetOneInternship(int id)
        {

            var internship = await _service.InternshipService.GetInternshipById(id, trackChanges: false);

            return Ok(internship);

        }

        [HttpPost("SavedOpportunity")]

        public async Task<IActionResult> SavedOpportunity([FromBody] SavedOpportunityDto savedOpportunityDto)
        {

             await _service.OpportunityService.SavedOpportunity(savedOpportunityDto);

            return Ok();

        }

        [HttpDelete("UnSavedOpportunity")]
        public async Task<IActionResult> UnSavedOpportunity([FromBody] SavedOpportunityDto savedOpportunityDto)
        {

            await _service.OpportunityService.DeleteOpportunity(savedOpportunityDto);

            return Ok();


        }
    }
}