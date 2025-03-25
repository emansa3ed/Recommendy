using Entities.GeneralResponse;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Contracts;
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
        public ChatController(IServiceManager serviceManager)
        {
            _service = serviceManager;

        }

        // [Authorize(Roles = "Student")]
        // [HttpPost("Students/{StudentId}/Posts/{PostId}")]

        [HttpGet("{secondUserId}")]
        public async Task<ActionResult> CreateOrGetChat([FromRoute] string secondUserId , [FromRoute]  string UserId)
        {


            var chat = await _service.ChatUsersService.GetChatByUserIds(UserId, secondUserId);
            return Ok(new ApiResponse<int> { Success = true, Message = "Fetch success", Data = chat.Id });

        }
    }
}