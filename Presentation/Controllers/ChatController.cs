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
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IServiceManager _service;
       private readonly UserManager<User> _userManager;

        public ChatController(IServiceManager serviceManager, UserManager<User> userManager)
        {
            _service = serviceManager;
            _userManager = userManager;


        }


        [HttpGet("{secondUserId}")]
        public async Task<ActionResult> GetChat([FromRoute] string secondUserId , [FromRoute]  string UserId)
        {


            var chat = await _service.ChatUsersService.GetChatByUserIds(UserId, secondUserId);
            if (chat == null)
                chat =  await CreateChat( secondUserId, UserId);

            return Ok(new ApiResponse<int> { Success = true, Message = "Fetch success", Data = chat.Id });

        }

       
       private async Task<ChatUsers> CreateChat(string secondUserId,  string UserId)
        {


            var chat = await _service.ChatUsersService.CreateChat(UserId, secondUserId);

            return chat;

        }


        [HttpPost("{ChatId}")]
        public async Task<ActionResult> SendMessage([FromRoute] int ChatId, [FromBody] ChatMessageDto chatMessageDto)
        {

            var user = await _userManager.GetUserAsync(User);
          
           // chatMessageDto.SenderId = user.Id;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


               await _service.ChatMessageService.SendMessage(ChatId, chatMessageDto);

            return Ok();

        }


    }
}