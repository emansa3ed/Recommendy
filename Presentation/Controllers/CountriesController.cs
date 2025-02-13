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
   // [Authorize]
    public class CountriesController : ControllerBase
    {

        private readonly IServiceManager _service;
        public CountriesController(IServiceManager service) => _service = service;

        [HttpGet]

        public IActionResult GetCountries()
        {
            try
            {
                var Countries =
                _service.CountryService.GetAllCountries(trackChanges: false);
                return Ok(Countries);

            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
