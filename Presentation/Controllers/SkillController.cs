using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{


	[Route("api/Skill")]
	[Authorize]
	[ApiController]
	public class SkillController : ControllerBase
	{
		private readonly IServiceManager _service;
		public SkillController(IServiceManager service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetSkills()
		{
			string UserName = User.Identity.Name;
			var user = await _service.UserService.GetDetailsByUserName(UserName);
			var skills = await _service.SkillService.GetSkillsAsync(user.Id);
			return Ok(skills);
		}

		[HttpPost("UploadResume")]
		public async Task<IActionResult> UploadResume([FromForm]IFormFile ResumeFile)
		{
			string UserName = User.Identity.Name;
			var user = await _service.UserService.GetDetailsByUserName(UserName);

			var Skills = await _service.ResumeParserService.UploadResume(ResumeFile);

			if (Skills == null || !Skills.Any())
				return Ok();

			return await AddSkills(Skills);
		}

		[HttpPost]
		public async Task<IActionResult> AddSkills([FromBody] List<string> skills)
		{
			string UserName = User.Identity.Name;
			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (skills == null || !skills.Any())
				return BadRequest("Skill list cannot be empty.");

			await _service.SkillService.AddSkillsAsync(user.Id, skills);
			return NoContent();
		}

		[HttpDelete("{skill}")]
		public async Task<IActionResult> RemoveSkill([FromRoute] string skill)
		{
			string UserName = User.Identity.Name;
			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (string.IsNullOrWhiteSpace(skill))
				return BadRequest("Skill cannot be empty.");

			await _service.SkillService.RemoveSkillAsync(user.Id, skill);
			return NoContent();
		}

	}
}
