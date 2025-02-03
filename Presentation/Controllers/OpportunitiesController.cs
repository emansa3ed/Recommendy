using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class OpportunitiesController : ControllerBase 
    {

         private readonly  IServiceManager _service;
        public OpportunitiesController (IServiceManager ServiceManager) { 
        
         _service = ServiceManager;
        
        }


    }
}
