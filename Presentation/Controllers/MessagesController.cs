using Entities.GeneralResponse;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Contracts;
using Shared.DTO.Chat;
using Shared.DTO.Internship;
using Shared.DTO.Report;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
	[Route("api/User/{UserId}/Chat/{ChatId}/[Controller]")]
	[ApiController]
	[Authorize]

	public class MessagesController : ControllerBase
	{
		private readonly IServiceManager _service;

		public MessagesController(IServiceManager serviceManager)
		{
			_service = serviceManager;


		}



		[HttpPost]
		public async Task<ActionResult> SendMessage([FromRoute] int ChatId, [FromRoute] string UserId,  [FromBody]string Message)
		{

			var CurrentUser  = await _service.UserService.GetDetailsByUserName(User.Identity.Name);



			if (!ModelState.IsValid)
				return BadRequest(ModelState);


			await _service.ChatMessageService.SendMessage(CurrentUser.Id, UserId, ChatId, Message);

			return Ok();

		}

	
		
 		[HttpGet]
		public async Task<ActionResult<ApiResponse<PagedList<ChatMessageDto>>>> GetChatMessages([FromRoute] int ChatId, [FromRoute] string UserId, [FromQuery] MessageParameters messageParameters)
		{
            var CurrentUser = await _service.UserService.GetDetailsByUserName(User.Identity.Name);


			var result =  await _service.ChatMessageService.GetChatMessages(ChatId, CurrentUser.Id, UserId, messageParameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.MetaData));

            return Ok(new ApiResponse<PagedList<ChatMessageDto>> { Success = true, Message = "Fetch success", Data = result });

        }

    }
}