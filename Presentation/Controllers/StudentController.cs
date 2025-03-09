using Entities.Exceptions;
using Entities.GeneralResponse;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Internship;
using Shared.DTO.Scholaship;
using Shared.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/students/{StudentId}")]
    [ApiController]
    [Authorize(Roles ="Student")]
    public class StudentController : ControllerBase
    {

        private readonly IServiceManager _service;
        public StudentController(IServiceManager ServiceManager)
        {

            _service = ServiceManager;

        }

   

        [HttpGet("profile")]
        public IActionResult GetStudent( [FromRoute]string StudentId)
        {
          
                var student = _service.StudentService.GetStudent(StudentId, trackChanges: false);

                 return Ok(new ApiResponse<StudentDto>
                {
                    Success = true,
                    Message = "fetch data  successfully.",
                    Data= student

                }); 
         
        }


        [HttpPatch("edit-profile")]
        public async Task<IActionResult> UpdateStudentProfile( [FromRoute] string StudentId, [FromBody] StudentForUpdateDto studentForUpdate)
        {

            await _service.StudentService.UpdateStudentProfileAsync(StudentId, studentForUpdate);
            return NoContent();

        }
       
       


        [HttpGet("saved-scholarships")]
        public async Task<IActionResult> GetSavedScholarships([FromRoute] string StudentId)
        {
            
                var savedScholarships = await _service.OpportunityService.GetSavedScholarshipsAsync(StudentId);

         return Ok(new ApiResponse<IEnumerable< GetScholarshipDto>> {  Success = true , Message = "ChangePassword successfully."  ,
             Data = savedScholarships });

        }

        [HttpGet("saved-internships")]
        public async Task<IActionResult> GetSavedInternships([FromRoute] string StudentId)
        {
            
            var savedInternships = await _service.OpportunityService.GetSavedInternshipsAsync(StudentId);

            return Ok(new ApiResponse<IEnumerable<InternshipDto>>
            {
                Success = true,
                Message = "fetch data  successfully.",
                Data = savedInternships
            });

        }

    }
}
