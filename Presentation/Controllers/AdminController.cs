using Entities.GeneralResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Company;
using Shared.DTO.User;
using Shared.DTO.University;
using Shared.RequestFeatures;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DTO.Admin;

namespace Presentation.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AdminController(IServiceManager service) => _service = service;

        [HttpGet("companies/unverified")]
        public async Task<IActionResult> GetUnverifiedCompanies(
    [FromQuery] CompanyParameters companyParameters)
        {
            var companies = await _service.CompanyService
                .GetUnverifiedCompaniesAsync(companyParameters, trackChanges: false);


            return Ok(companies);
        }

        [HttpPatch("companies/{id}/verify")]
        public async Task<IActionResult> VerifyCompany(
            string id,
            [FromBody] CompanyVerificationDto verificationDto)
        {
            await _service.CompanyService.VerifyCompany(id, verificationDto, trackChanges: true);
            return NoContent();
        }
        [HttpGet("universities/unverified")]
        public async Task<IActionResult> GetUnverifiedUniversities(
    [FromQuery] UniversityParameters universityParameters)
        {
            var universities = await _service.UniversityService
                .GetUnverifiedUniversitiesAsync(universityParameters, trackChanges: false);


            return Ok(universities);
        }

        [HttpPatch("universities/{id}/verify")]
        public async Task<IActionResult> University(string id, [FromBody] UniversityVerificationDto verificationDto)
        {
            await _service.UniversityService.VerifyUniversity(id, verificationDto, trackChanges: true);
            return NoContent();
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] UsersParameters parameters)
        {
            var users = await _service.AdminService
                .GetUsersAsync(parameters, trackChanges: false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(users.MetaData));

            return Ok(new ApiResponse<PagedList<UserDto>>
            {
                Success = true,
                Data = users
            });
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _service.AdminService
                .GetUserByIdAsync(id, trackChanges: false);

            return Ok(new ApiResponse<UserDto>
            {
                Success = true,
                Data = user
            });
        }

        [HttpPatch("users/{id}/ban")]
        public async Task<IActionResult> BanUser(
            string id,
            [FromBody] UserBanDto banDto)
        {
            await _service.AdminService.BanUserAsync(id, banDto, trackChanges: true);

            return NoContent();
        }

        [HttpPatch("users/{id}/unban")]
        public async Task<IActionResult> UnbanUser(string id)
        {
            await _service.AdminService.UnbanUserAsync(id, trackChanges: true);
            return NoContent();
        }
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _service.AdminService.DeleteUserAsync(id);
            return NoContent(); 
        }



        [HttpGet("dashboard/statistics")]
        public async Task<IActionResult> GetDashboardStatistics()
        {

            var stats = await _service.AdminService.GetDashboardStatisticsAsync();

            return Ok(new ApiResponse<AdminDashboardStatsDto>
            {
                Success = true,
                Message = "Dashboard statistics retrieved successfully",
                Data = stats,
            });
        }
    }
}
