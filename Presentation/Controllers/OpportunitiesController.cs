using Entities.GeneralResponse;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Service;
using Service.Contracts;
using Service.Ontology;
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
	[Authorize(Roles = "Student")]
	public class OpportunitiesController : ControllerBase
	{

		private readonly IServiceManager _service;
		private readonly MyMemoryCache _memoryCache;
		public OpportunitiesController(IServiceManager ServiceManager,MyMemoryCache memoryCache)
		{
			_service = ServiceManager;
			_memoryCache = memoryCache;
		}


		[HttpGet]
		public async Task<IActionResult> GetOpportunities([FromQuery] OpportunitiesParameters OpportunitiesParameters)
		{
			var username = User.Identity.Name;
			var user = await _service.UserService.GetDetailsByUserName(username);
			var UserSkills =  _service.StudentService.GetStudent(user.Id, trackChanges: false).Skills;

			var skills = UserSkills
						.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
						.Select(term => term.ToLower())
						.ToList();

			var ExpandedSkills = SkillOntology.ExpandSkills(skills);
			var NewSkills = string.Join(",", ExpandedSkills.ToList());

			var scholarshipsParameters = new ScholarshipsParameters
			{
				PageNumber = OpportunitiesParameters.PageNumber,
				PageSize = OpportunitiesParameters.PageSize,
			};

			var internshipParameters = new InternshipParameters
			{
				PageNumber = OpportunitiesParameters.PageNumber,
				PageSize = OpportunitiesParameters.PageSize,
			};

			string Titles;
			if (!_memoryCache.Cache.TryGetValue(scholarshipsParameters.ToString() + UserSkills + "GetAllRecommendedScholarships", out PagedList<Scholarship> cacheValue))
			{
				Titles = _service.GeminiService.SendRequest($"Given the following skills: {NewSkills}," +
					$" identify the top 10 general titles that are commonly found in the name or description of relevant scholarships or internships." +
					$" Avoid combining skills with titles (e.g., avoid 'Python Developer'). Only return general," +
					$" role-based titles such as 'Developer', 'Analyst', or 'Researcher'." +
					$"Do not use keywords like 'intern' or 'scholarship' directly" +
					$" Format the result exactly as: Title1, Title2, Title3, Title4, Title5, Title6, Title7, Title8, Title9, Title10." +
					$" Do not include any additional text or explanations.");
			}
			else
			{
				Titles = null;
			}


			var scholarships = await _service.ScholarshipService.GetAllRecommendedScholarships(UserSkills, Titles, scholarshipsParameters, trackChanges: false);

			var internships = await _service.InternshipService.GetAllRecommendedInternships(UserSkills, Titles, internshipParameters, trackChanges: false);

			MetaData metaData = new MetaData
			{
				PageSize = OpportunitiesParameters.PageSize,
				CurrentPage = OpportunitiesParameters.PageNumber,
				TotalCount = scholarships.MetaData.TotalCount+internships.MetaData.TotalCount,
				TotalPages = (int)Math.Ceiling((double)(scholarships.MetaData.TotalCount + internships.MetaData.TotalCount) / OpportunitiesParameters.PageSize),
			};
			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));

			GetOpportunitiesDto opportunities = new GetOpportunitiesDto
			{
				Scholarships = scholarships,
				Internships = internships
			};
			return Ok(new ApiResponse<GetOpportunitiesDto> {Data= opportunities,Success=true});

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

		[HttpPost("SaveOpportunity")]

		public async Task<IActionResult> SavedOpportunity([FromBody] SavedOpportunityDto savedOpportunityDto)
		{
			if (savedOpportunityDto == null)
				return BadRequest("SavedOpportunityDto is required");
			string UserName = User.Identity.Name;
			var user = await _service.UserService.GetDetailsByUserName(UserName); 

			await _service.OpportunityService.SavedOpportunity(user.Id, savedOpportunityDto);

			return Ok();

		}

		[HttpDelete("UnSaveOpportunity")]
		public async Task<IActionResult> UnSavedOpportunity([FromBody] SavedOpportunityDto savedOpportunityDto)
		{
			if (savedOpportunityDto == null)
				return BadRequest("SavedOpportunityDto is required");
			string UserName = User.Identity.Name;
			var user = await _service.UserService.GetDetailsByUserName(UserName);
			await _service.OpportunityService.DeleteOpportunity(user.Id, savedOpportunityDto);

			return Ok();


		}
	}
}