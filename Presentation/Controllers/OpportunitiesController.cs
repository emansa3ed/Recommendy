using Entities.GeneralResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Internship;
using Shared.DTO.opportunity;
using Shared.DTO.Scholaship;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
	[Route("api/[Controller]")]
	[ApiController]
	//[Authorize(Roles = "Student")]
	public class OpportunitiesController : ControllerBase
	{

		private readonly IServiceManager _service;
		public OpportunitiesController(IServiceManager ServiceManager)
		{

			_service = ServiceManager;

		}

		[HttpGet("Scholarships/all")]
		public async Task<IActionResult> GetScholarships([FromQuery] ScholarshipsParameters scholarshipsParameters)
		{

			var scholarships = await _service.ScholarshipService.GetAllScholarships(scholarshipsParameters, trackChanges: false);
			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(scholarships.MetaData));

            return Ok(new ApiResponse<PagedList<GetScholarshipDto>> { Success = true, Message = "Fetch success", Data = scholarships });

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
		public async Task<IActionResult> GetInternships([FromQuery] InternshipParameters internshipParameters)
		{

			var internships = await _service.InternshipService.GetAllInternships(internshipParameters, trackChanges: false);
			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(internships.MetaData));

            return Ok(new ApiResponse<PagedList<InternshipDto>> { Success = true, Message = "Fetch success", Data = internships });

        }


        [HttpGet("Internships/{id:int}")]
		public async Task<ActionResult<InternshipDto>> GetOneInternship(int id)
		{

			var internship = await _service.InternshipService.GetInternshipById(id, trackChanges: false);

			return Ok(internship);

		}

		[HttpPost("SavedOpportunity/Student/{StudentId}")]

		public async Task<IActionResult> SavedOpportunity([FromRoute] string StudentId, [FromBody] SavedOpportunityDto savedOpportunityDto)
		{

			await _service.OpportunityService.SavedOpportunity(StudentId, savedOpportunityDto);

			return Ok();

		}

		[HttpDelete("UnSavedOpportunity/Student/{StudentId}")]
		public async Task<IActionResult> UnSavedOpportunity([FromRoute] string StudentId, [FromBody] SavedOpportunityDto savedOpportunityDto)
		{

			await _service.OpportunityService.DeleteOpportunity(StudentId, savedOpportunityDto);

			return Ok();


		}
	}
}