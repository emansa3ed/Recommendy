using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IServiceManager _service;
        public ChatController(  IServiceManager serviceManager) 
        { 
            _service = serviceManager;
       
        }


       

    }
}
