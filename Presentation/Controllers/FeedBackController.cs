using Entities.GeneralResponse;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Feedback;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
	[Route("api/Companies/{CompanyID}/Posts/{PostId}/[controller]")]
	[ApiController]
	[Authorize(Roles = "Student")]
	public class FeedBackController : ControllerBase
	{
		private readonly IServiceManager _service;

		public FeedBackController(IServiceManager service)
		{ 
			_service = service ?? throw new ArgumentNullException(nameof(service));
		}


		[HttpGet("{feedbackId}")]
		public async Task<ActionResult<ApiResponse<FeedBackDto>>> GetFeedBack([FromRoute] string CompanyID, [FromRoute] int PostId, [FromRoute] int feedbackId)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var res = await _service.FeedbackService.GetFeedbackAsync(feedbackId);
			return Ok(new ApiResponse<FeedBackDto> { Success=true,Data=res});
		}

		[HttpGet]
		public async Task<ActionResult<ApiResponse<PagedList<FeedBackDto>>>> GetAllFeedBacks([FromRoute] string CompanyID, [FromRoute] int PostId, [FromQuery]FeedBackParameters feedBack)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			
			var res = await _service.FeedbackService.GetAllFeedbackAsync(CompanyID, PostId, feedBack);

			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(res.MetaData));


			return Ok(new ApiResponse<PagedList<FeedBackDto>> { Success = true, Data = res });
		}

		[HttpPost]
		public async Task<ActionResult> CreateFeedBack([FromRoute]string CompanyID, [FromRoute] int PostId, FeedbackCreationDto feedback)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			await _service.FeedbackService.CreateFeedbackAsync(CompanyID, PostId, feedback.StudentId, feedback);
			return Ok();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteFeedBack([FromRoute] string CompanyID, [FromRoute] int PostId, FeedbackDelationDto feedback)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			await _service.FeedbackService.DeleteFeedbackAsync(CompanyID, PostId, feedback);
			return NoContent();
		}

	}
}
