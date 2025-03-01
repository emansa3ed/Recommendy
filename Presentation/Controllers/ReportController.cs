using Microsoft.AspNetCore.Authorization;
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
namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 //   [Authorize(Roles = "Student")]
    public class ReportController : ControllerBase
    {
        private readonly IServiceManager _service;


     public   ReportController(IServiceManager service )   
        {
             _service = service ?? throw new ArgumentNullException(nameof(service));
            

        }

        [HttpPost("Students/{StudentId}/Posts/{PostId}")]

        public async  Task<ActionResult>  CreateReport( [FromRoute]string StudentId , [FromRoute]int PostId , ReportDtoCreation reportDtoCreation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _service.ReportService.CreateReport(StudentId, PostId, reportDtoCreation);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedList<ReportDto>>>> GetEmployeesForCompany([FromQuery] ReportParameters reportParameters)
        {
            var pagedResult = await _service.ReportService.GetReportsAsync(reportParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));


            return Ok(new ApiResponse<PagedList<ReportDto>> { Success = true, Message ="Fetch success",Data = pagedResult }); ;

          
        }

    }
}
