using Entities.Exceptions;
using Entities.GeneralResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Company;
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
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CompaniesController(IServiceManager service) => _service = service;


        [HttpGet("profile/{id}")]
        [Authorize]
        public IActionResult GetCompany(string id)
        {
                var company = _service.CompanyService.GetCompany(id, trackChanges: false);
                return Ok(company);
           
        }


        [HttpPatch("edit-profile/{id}")]
		[Authorize(Roles = "Company")]
		public async Task<IActionResult> EditProfile(string id, [FromBody] CompanyDto companyDto)
        {

			var UserName = User.Identity?.Name;

			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (user.Id != id)
				return BadRequest(new ApiResponse<Internship> { Success = false, Message = "CompanyID don't match with authorized one", Data = null });

			await _service.CompanyService.UpdateCompany(id, companyDto, trackChanges: true);
            return NoContent();
        }

        
        

    }
}
