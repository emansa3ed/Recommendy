using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Exceptions;
using Entities.Models;
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Service.Contracts;
using Shared.DTO;
using Microsoft.AspNetCore.Identity;



namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmailsController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly UserManager<User> _userManager;
        public EmailsController(IServiceManager service, UserManager<User> userManage)
        {
            _service = service;
            _userManager = userManage;

        }


        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequestDto request)
        {
            var result = await _service.EmailsService.Sendemail(request.Email, request.Message, request.Reason);
            return Ok(result);
        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string Usercode)
        {
            try
            {
                var result = await _service.EmailsService.ConfirmEmailAsync(userId, Usercode);
                return Ok(result);

            }
            catch (Exception ex)
            {

                return BadRequest($"Error confirming email. {ex.Message} | Inner Exception: {ex.InnerException?.Message}");


            }

        }
        [HttpPost("GenerateUserCode")]
        public async Task<IActionResult> GenerateUserCode(string userId)
        {
            try
            {
                var result = await _service.userCodeService.GenerateUserCodeForConfirmtationAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Generate UserCode . {ex.Message} | Inner Exception: {ex.InnerException?.Message}");


            }

        }
    }
}
