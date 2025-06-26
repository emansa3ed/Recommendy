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
		public OpportunitiesController(IServiceManager ServiceManager, MyMemoryCache memoryCache)
		{
			_service = ServiceManager;
			_memoryCache = memoryCache;
		}


		[HttpGet("RecommendedScholarships")]
		public async Task<IActionResult> GetOpportunities([FromQuery] ScholarshipsParameters scholarshipsParameters)
		{
			var username = User.Identity.Name;
			var user = await _service.UserService.GetDetailsByUserName(username);
			var UserSkills = _service.StudentService.GetStudent(user.Id, trackChanges: false).Skills;



			string scholars;
			if (!_memoryCache.Cache.TryGetValue(scholarshipsParameters.ToString() + UserSkills + "GetAllRecommendedScholarships", out PagedList<Scholarship> cacheValue))
			{

				var res2 = await _service.ScholarshipService.GetAllScholarships(new ScholarshipsParameters { PageSize = 50 }, false);
				var scholarsJson = JsonSerializer.Serialize(res2);

				scholars = _service.GeminiService.SendRequest(
					$"You are an intelligent assistant that recommends relevant scholarships based on skills." +
					$"\n\nSkills: {UserSkills}" +
					$"\n\nScholarship data (as JSON): {scholarsJson}" +
					$"\n\nFrom the above list, analyze the scholarship data and return the IDs of the top 10 scholarships that best match the given skills." +
					$"\nOnly consider the fields that are relevant to matching (e.g., name, description, tags, etc.)." +
					$"\nReturn only a comma-separated list of scholarship IDs like this: id1, id2, id3, ..., id10." +
					$"\nDo not include any explanation, titles, or extra text — only the IDs.");
			}
			else
			{
				scholars = null;
			}


			var scholarships = await _service.ScholarshipService.GetAllRecommendedScholarships(UserSkills, scholars, scholarshipsParameters, trackChanges: false);


			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(scholarships.MetaData));

			return Ok(new ApiResponse<List<GetScholarshipDto>> { Data = scholarships, Success = true });

		}


		[HttpGet("RecommendedInternships")]
		public async Task<IActionResult> GetOpportunities([FromQuery] InternshipParameters internshipParameters)
		{
			var username = User.Identity.Name;
			var user = await _service.UserService.GetDetailsByUserName(username);
			var UserSkills = _service.StudentService.GetStudent(user.Id, trackChanges: false).Skills;


			string interns;
			if (!_memoryCache.Cache.TryGetValue(internshipParameters.ToString() + UserSkills + "GetAllRecommendedScholarships", out PagedList<Scholarship> cacheValue))
			{
				var res = await _service.InternshipService.GetAllInternships(new InternshipParameters { PageSize = 50 }, false);
				var internsJson = JsonSerializer.Serialize(res);


				interns = _service.GeminiService.SendRequest(
					$"You are an intelligent assistant that recommends relevant internships based on skills." +
					$"\n\nSkills: {UserSkills}" +
					$"\n\nInternship data (as JSON): {internsJson}" +
					$"\n\nFrom the above list, analyze the internship data and return the IDs of the top 10 internships that best match the given skills." +
					$"\nOnly consider the fields that are relevant to matching (e.g., name, description, tags, etc.)." +
					$"\nReturn only a comma-separated list of internship IDs like this: id1, id2, id3, ..., id10." +
					$"\nDo not include any explanation, titles, or extra text — only the IDs.");
			}
			else
			{
				interns = null;
			}



			var internships = await _service.InternshipService.GetAllRecommendedInternships(UserSkills, interns, internshipParameters, trackChanges: false);

			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(internships.MetaData));


			return Ok(new ApiResponse<List<InternshipDto>> { Data = internships, Success = true });

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