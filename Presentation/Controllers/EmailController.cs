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



namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmailsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmailsController(IServiceManager serviceManage)
        {
             _serviceManager = serviceManage;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequestDto request)
        {
            var result = await _serviceManager.EmailsService.Sendemail(request.Email, request.Message,request.Reason);
            return Ok(result);
        }
    }
}
