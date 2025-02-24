using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
	[Route("api/Companies/{CompanyID}/Posts/{PostId}/Students/{StudentId}/[controller]")]
	[ApiController]
	public class FeedBackController : ControllerBase
	{
		private readonly IServiceManager _service;

		public FeedBackController(IServiceManager service)
		{
			_service = service ?? throw new ArgumentNullException(nameof(service));
		}

		[HttpPost]
		public async Task<ActionResult> CreateFeedBack([FromRoute]string CompanyID, [FromRoute] int PostId,[FromRoute] string StudentId, FeedbackCreationDto feedback)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			await _service.FeedbackService.CreateFeedbackAsync(CompanyID, PostId,StudentId, feedback);
			return Ok();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteFeedBack([FromRoute] string CompanyID, [FromRoute] int PostId, [FromRoute] string StudentId, FeedbackDelationDto feedback)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			await _service.FeedbackService.DeleteFeedbackAsync(CompanyID, PostId, StudentId, feedback);
			return NoContent();
		}

	}
}
