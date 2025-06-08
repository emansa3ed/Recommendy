using Azure;
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
	[Route("api/Receiver/{ReceiverID}/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly IServiceManager _service;

		public NotificationController(IServiceManager service)
		{
			_service = service ?? throw new ArgumentNullException(nameof(service));
		}


		[HttpGet("{NotificationId}")]
		public async Task<ActionResult<ApiResponse<NotificationDto>>> GetNotification([FromRoute] string ReceiverID, int NotificationId)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var res = await _service.NotificationService.GetNotificationAsync(ReceiverID, NotificationId);
			return Ok(new ApiResponse<NotificationDto> { Success = true, Data = res });
		}

		[HttpGet]
		public async Task<ActionResult<ApiResponse<PagedList<NotificationDto>>>> GetAllFeedBacks([FromRoute] string ReceiverID, [FromQuery] NotificationParameters notification)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var res = await _service.NotificationService.GetAllNotificationskAsync(ReceiverID, notification, false);

			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(res.MetaData));


			return Ok(new ApiResponse<PagedList<NotificationDto>> { Success = true, Data = res });

		}
	}
}
