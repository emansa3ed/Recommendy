using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Microsoft.AspNetCore.Authorization;


namespace Presentation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class CountriesController : ControllerBase
    {

        private readonly IServiceManager _service;
        public CountriesController(IServiceManager service) => _service = service;
       
        [HttpGet]
  
        public IActionResult GetCompanies()
        {
            try
            {
                var companies =
                _service.CountryService.GetAllCountries(trackChanges: false);
                return Ok(companies);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
