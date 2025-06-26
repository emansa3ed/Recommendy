using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System.Threading.Tasks;
using Entities.GeneralResponse;
using Shared.DTO.Course;
using System.Collections.Generic;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public CourseController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("by-internship/{internshipId}")]
        public async Task<IActionResult> GetCoursesByInternship(int internshipId)
        {
            var courses = await _serviceManager.CourseService.GetCoursesForInternshipAsync(internshipId);
            var response = new ApiResponse<List<CourseWithScoreDto>>
            {
                Success = true,
                Message = courses.Count > 0 ? "Courses found." : "No courses found.",
                Data = courses
            };
            return Ok(response);
        }

        [HttpGet("by-scholarship/{scholarshipId}")]
        public async Task<IActionResult> GetCoursesByScholarship(int scholarshipId)
        {
            var courses = await _serviceManager.CourseService.GetCoursesForScholarshipAsync(scholarshipId);
            var response = new ApiResponse<List<CourseWithScoreDto>>
            {
                Success = true,
                Message = courses.Count > 0 ? "Courses found." : "No courses found.",
                Data = courses
            };
            return Ok(response);
        }
    }
} 