using Entities.GeneralResponse;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Contracts;
using Shared.DTO.Chat;
using Shared.DTO.Report;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/User/{UserId}/[Controller]")]
	[Authorize(Roles = "PremiumUser")]
	[Authorize] 

    public class ChatController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ChatController(IServiceManager serviceManager)
        {
            _service = serviceManager;
        }


        [HttpGet]
        public async Task<ActionResult> GetChat([FromRoute]  string UserId)
        {

            var CurrentUser = await _service.UserService.GetDetailsByUserName(User.Identity.Name);

			var chat = await _service.ChatUsersService.GetChatByUserIds(UserId, CurrentUser.Id);
            if (chat == null)
                chat =  await CreateChat(CurrentUser.Id, UserId);

            return Ok(new ApiResponse<int> { Success = true, Message = "Fetch success", Data = chat.Id });

        }

       
       private async Task<ChatUsers> CreateChat(string secondUserId,  string UserId)
        {


            var chat = await _service.ChatUsersService.CreateChat(UserId, secondUserId);

            return chat;

        }



    }
}