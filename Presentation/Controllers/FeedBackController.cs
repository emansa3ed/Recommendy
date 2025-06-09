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

	[Route("api/Organization/{OrganizationID}/Posts/{PostId}/[controller]")]
	[ApiController]
	public class FeedBackController : ControllerBase
	{
		private readonly IServiceManager _service;


		public FeedBackController(IServiceManager service)
		{
			_service = service ?? throw new ArgumentNullException(nameof(service));
		}



		[HttpGet("{feedbackId}")]
		[Authorize]
		public async Task<ActionResult<ApiResponse<FeedBackDto>>> GetFeedBack([FromRoute] string OrganizationID, [FromRoute] int PostId, [FromRoute] int feedbackId)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var res = await _service.FeedbackService.GetFeedbackAsync(feedbackId);
			return Ok(new ApiResponse<FeedBackDto> { Success = true, Data = res });
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult<ApiResponse<PagedList<FeedBackDto>>>> GetAllFeedBacks([FromRoute] string OrganizationID, [FromRoute] int PostId, [FromQuery] FeedBackParameters feedBack)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var res = await _service.FeedbackService.GetAllFeedbackAsync(OrganizationID, PostId, feedBack);


			 res = await _service.FeedbackService.GetAllFeedbackAsync(OrganizationID, PostId, feedBack);

			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(res.MetaData));
            return Ok(new ApiResponse<PagedList<FeedBackDto>> { Success = true, Data = res });

        }

			[HttpPost]
			[Authorize(Roles = "Student")]
			public async Task<ActionResult> CreateFeedBack([FromRoute] string OrganizationID, [FromRoute] int PostId, FeedbackCreationDto feedback)
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);
				var user = await _service.UserService.GetDetailsByUserName(User.Identity.Name);
				await _service.FeedbackService.CreateFeedbackAsync(OrganizationID, PostId, user.Id, feedback);
				return Ok();
			}

			[HttpDelete]
			[Authorize(Roles = "Student")]
			public async Task<ActionResult> DeleteFeedBack([FromRoute] string OrganizationID, [FromRoute] int PostId, FeedbackDelationDto feedback)
			{

				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var user = await _service.UserService.GetDetailsByUserName(User.Identity.Name);

				await _service.FeedbackService.DeleteFeedbackAsync(OrganizationID, user.Id, PostId, feedback);
				return NoContent();
			}


			

		
	}
}