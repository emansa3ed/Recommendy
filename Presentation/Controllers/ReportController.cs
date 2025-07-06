﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.RequestFeatures;
using Entities.GeneralResponse;
using Shared.DTO.Feedback;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities.Exceptions;
using Entities.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IServiceManager _service;


        public ReportController(IServiceManager service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));


        }

        [Authorize(Roles = "Student")]
        [HttpPost("Students/{StudentId}/Posts/{PostId}")]

        public async Task<ActionResult> CreateReport([FromRoute] string StudentId, [FromRoute] int PostId, ReportDtoCreation reportDtoCreation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
			var UserName = User.Identity?.Name;

			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (user.Id != StudentId)
				return BadRequest(new ApiResponse<Internship> { Success = false, Message = "StudentId don't match with authorized one", Data = null });
			await _service.ReportService.CreateReport(StudentId, PostId, reportDtoCreation);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<PagedList<ReportDto>>>> GetReports([FromQuery] ReportParameters reportParameters)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            var pagedResult = await _service.ReportService.GetReportsAsync(reportParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));


            return Ok(new ApiResponse<PagedList<ReportDto>> { Success = true, Message = "Fetch success", Data = pagedResult }); ;


        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{ReportId}")]
        public async Task<ActionResult<ApiResponse<ReportDto>>> GetReport( [FromRoute]int ReportId)
        {
            var report  =  await _service.ReportService.GetReport(ReportId);

            return Ok(new ApiResponse<ReportDto> { Success = true, Message = "Fetch success", Data = report }); 

        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{ReportId}")]
        public async Task<ActionResult> DeleteReport( [FromRoute]  int ReportId)
        {
           await _service.ReportService.DeleteReport(ReportId);  

            return NoContent();

        }



        // Admin endpoint to Review report status and optionally delete the post
        [Authorize(Roles = "Admin")]
        [HttpPatch("{ReportId}")]


        public async Task<IActionResult> UpdateReportStatus(int ReportId, [FromBody] UpdateReportStatusDto dto)
        {
            
            
                var report = await _service.ReportService.GetReport(ReportId);

                await _service.ReportService.UpdateReportStatus(ReportId, dto);

                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Message = "Report status updated successfully.",
                    Data = null
                });
            
            
        }
    
    }

}

