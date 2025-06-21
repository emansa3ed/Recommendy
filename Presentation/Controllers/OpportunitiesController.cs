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


		[HttpGet("RecommendedScholarships")]
		public async Task<IActionResult> RecommendedScholarships([FromQuery] ScholarshipsParameters scholarshipsParameters)
		{
			var username = User.Identity.Name;
			var user = await _service.UserService.GetDetailsByUserName(username);
			var UserSkills =  _service.StudentService.GetStudent(user.Id, trackChanges: false).Skills;

			if (!_memoryCache.Cache.TryGetValue(scholarshipsParameters.ToString() + UserSkills + "GetAllRecommendedScholarshipsIDS", out string IDS))
			{
				var res = await _service.ScholarshipService.GetAllScholarships(new ScholarshipsParameters { PageSize = 50 }, false);
				var res2 = res.Select(r => new { r.Description, r.Name, r.Id });
				var scholarsJson = JsonSerializer.Serialize(res2);

				List<string> chunks = ChunkJsonString(scholarsJson, 1000); 

				var tasks = chunks.Select(async chunk =>
				{
					var prompt =
						"You are an intelligent assistant that recommends relevant scholarships based on skills." +
						$"\n\nSkills: {UserSkills}" +
						$"\n\nHere is a portion of the scholarship data (as JSON): {chunk}" +
						"\n\nFrom the above list, return only the IDs of scholarships that best match the given skills." +
						"\nOnly consider fields that are relevant to matching (e.g., name, description)." +
						"\nReturn only a comma-separated list of scholarship IDs like this: id1, id2, id3, ..." +
						"\nDo not include any explanation, titles, or extra text — only the IDs.";

						var idsChunk = await _service.OllamaService.RecommendedOpportunities(
							prompt,
							"gemma3:4b-it-q8_0",
							false,
							null,
							"recommendation"
						);

						return idsChunk;

				}).ToList();

				var results = await Task.WhenAll(tasks);

				var allIds = results
					.Where(r => !string.IsNullOrWhiteSpace(r))
					.SelectMany(r => r.Split(',', StringSplitOptions.RemoveEmptyEntries)
						.Select(id => id.Trim()))
					.Distinct()
					.ToList();

				IDS = string.Join(", ", allIds);


				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSize(IDS.Length)
					.SetSlidingExpiration(TimeSpan.FromSeconds(240))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(320));

				_memoryCache.Cache.Set(scholarshipsParameters.ToString() + UserSkills + "GetAllRecommendedScholarshipsIDS", IDS, cacheEntryOptions);
			}

			var scholarships = await _service.ScholarshipService.GetAllRecommendedScholarships(UserSkills, IDS, scholarshipsParameters, trackChanges: false);


			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(scholarships.MetaData));

			return Ok(new ApiResponse<List<GetScholarshipDto>> {Data= scholarships, Success=true});

		}
		private List<string> ChunkJsonString(string json, int maxChunkSize)
		{
			var chunks = new List<string>();
			int totalLength = json.Length;

			for (int i = 0; i < totalLength; i += maxChunkSize)
			{
				int length = Math.Min(maxChunkSize, totalLength - i);
				chunks.Add(json.Substring(i, length));
			}

			return chunks;
		}



		[HttpGet("RecommendedInternships")]
		public async Task<IActionResult> RecommendedInternships([FromQuery] InternshipParameters internshipParameters)
		{
			var username = User.Identity.Name;
			var user = await _service.UserService.GetDetailsByUserName(username);
			var UserSkills = _service.StudentService.GetStudent(user.Id, trackChanges: false).Skills;

			if (!_memoryCache.Cache.TryGetValue(internshipParameters.ToString() + UserSkills + "GetAllRecommendedInternshipsIDS", out string IDS))
			{
				var res = await _service.InternshipService.GetAllInternships(new InternshipParameters { PageSize = 50 }, false);
				var res2 = res.Select(r => new { r.Description, r.Name,r.Id });
				var internsJson = JsonSerializer.Serialize(res2);

				List<string> chunks = ChunkJsonString(internsJson, 1000); 

				var tasks = chunks.Select(async chunk =>
				{
					var prompt =
						"You are an intelligent assistant that recommends relevant internships based on skills." +
						$"\n\nSkills: {UserSkills}" +
						$"\n\nHere is a portion of the internship data (as JSON): {chunk}" +
						"\n\nFrom the above list, return only the IDs of internships that best match the given skills." +
						"\nOnly consider fields that are relevant to matching (e.g., name, description)." +
						"\nReturn only a comma-separated list of internship IDs like this: id1, id2, id3, ..." +
						"\nDo not include any explanation, titles, or extra text — only the IDs.";
				
						var idsChunk = await _service.OllamaService.RecommendedOpportunities(
							prompt,
							"gemma3:4b-it-q8_0",
							false,
							null,
							"recommendation"
						);

						return idsChunk;

				}).ToList();

				var results = await Task.WhenAll(tasks);

				var allIds = results
					.Where(r => !string.IsNullOrWhiteSpace(r))
					.SelectMany(r => r.Split(',', StringSplitOptions.RemoveEmptyEntries)
						.Select(id => id.Trim()))
					.Distinct()
					.ToList();



				IDS = string.Join(", ", allIds);

				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSize(IDS.Length)
					.SetSlidingExpiration(TimeSpan.FromSeconds(240))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(320));

				_memoryCache.Cache.Set(internshipParameters.ToString() + UserSkills + "GetAllRecommendedInternshipsIDS", IDS, cacheEntryOptions);

			}


			var internships = await _service.InternshipService.GetAllRecommendedInternships(UserSkills, IDS, internshipParameters, trackChanges: false);

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