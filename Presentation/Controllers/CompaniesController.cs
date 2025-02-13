using Entities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers

{
    [Route("api/companies")]
    [ApiController]
    [Authorize(Roles ="Company")]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CompaniesController(IServiceManager service) => _service = service;


        [HttpGet("profile/{id}")]
        public IActionResult GetCompany(string id)
        {
            try
            {
                var company = _service.CompanyService.GetCompany(id, trackChanges: false);
                return Ok(company);
            }
            catch (CompanyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpPatch("edit-profile/{id}")]

        public async Task<IActionResult> EditProfile(string id, [FromBody] CompanyDto companyDto)
        {
            var username = User.Identity.Name;
            if (username == null)
            {
                return Unauthorized();
            }
            var user = await _service.UserService.GetDetailsByUserName(username);
            if (user == null)
            {
                return NotFound();
            }
            // only allowed for university 
            if (!user.Discriminator.Equals("Company", StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }
            await _service.CompanyService.UpdateCompany(id, companyDto, trackChanges: true);
            return NoContent();
        }

        
        

    }
}
