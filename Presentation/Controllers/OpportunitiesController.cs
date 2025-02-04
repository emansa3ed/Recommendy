using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;
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

        [HttpGet("Scholarships/all")]
        public async Task<IActionResult> GetScholarships()
        {
            try
            {
                var scholarships = await _service.ScholarshipService.GetAllScholarships(trackChanges: false);
                return Ok(scholarships);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use ILogger here)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("Scholarships/{id:int}")]
        public async Task<ActionResult<GetScholarshipDto>> GetOneScholarship(int id)
        {
            try
            {
                var scholarship = await _service.ScholarshipService.GetScholarshipById(id, trackChanges: false);
                if (scholarship == null)
                    return NotFound($"Scholarship with ID: {id} not found");
                return Ok(scholarship);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error while retrieving scholarship");
            }
        }



        [HttpGet("Internships/all")]
        public async Task<IActionResult> GetInternships()
        {
            try
            {
                var internships = await _service.InternshipService.GetAllInternships(trackChanges: false);
                return Ok(internships);
            }
            catch (Exception ex)
            { 
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("Internships/{id:int}")]
        public async Task<ActionResult<InternshipDto>> GetOneInternship(int id)
        {
            try
            {
                var internship = await _service.InternshipService.GetInternshipById(id, trackChanges: false);
                if (internship == null)
                    return NotFound($"Internship with ID: {id} not found");
                return Ok(internship);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while retrieving scholarship");
            }
        }
    }
}
