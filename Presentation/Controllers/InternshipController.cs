using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.GeneralResponse;
using Shared.DTO.Internship;
using Entities.Models;


namespace Presentation.Controllers
{
    [Route("api/Companies/{CompanyID}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Company")]

    public class InternshipController : ControllerBase
    {

        private readonly IServiceManager _service;
     
        public InternshipController( IServiceManager service) {

            _service = service; 
           
        
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse<Internship>>> CreateInternship( [FromRoute] string CompanyID ,[FromForm] InternshipCreationDto internshipCreation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.InternshipService.CreateInternship(CompanyID, internshipCreation);
              
                return Ok(  new ApiResponse<Internship>
                {
                    Success = true,
                    Message = "Internship created successfully.",
                    Data = result
                });      
        }





        [HttpPost("{InternshipId}/Positions")]
        public async Task<IActionResult> CreatePosition([FromRoute] string CompanyID, [FromRoute] int InternshipId, [FromBody]InternshipPositionDto internshipPositionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =   await _service.InternshipPosition.CreateInternshipPosition(CompanyID, InternshipId, internshipPositionDto);


            return Ok(new ApiResponse<InternshipPosition>
            {
                Success = true,
                Message = "InternshipPosition created successfully.",
                Data = result
            });


        }
        [HttpGet("StoredPositions")]
        public async Task<IActionResult> GetPosition([FromRoute] string CompanyID)
        {
            
                var result =   _service.PositionService.GetAllPositions(CompanyID, false);

            return Ok(new ApiResponse<IQueryable<Position>>
            {
                Success = true,
                Message = "Fetch Positions .",
                Data = result
            });



        }

        [HttpGet]
        public async Task<IActionResult> GetInternshipByCompanyId([FromRoute] string CompanyID)
        {
            var internshipDto = await _service.InternshipService.GetInternshipsByCompanyId(CompanyID);

            return Ok(new ApiResponse  <List<InternshipDto>>
            {
                Success = true,
                Message = "Fetch Positions .",
                Data = internshipDto
            });

        }


        [HttpDelete("{InternshipId}")]

        public async Task<IActionResult> DeleteInternship([FromRoute] string CompanyID, [FromRoute] int InternshipId)
        {

          

               await _service.InternshipService.DeleteInternship(CompanyID, InternshipId, false);

                return NoContent();
           
          

        }

        [HttpDelete("{InternshipId}/positions{PositionId}")]
        public  async Task<IActionResult> DeletePosition([FromRoute] string CompanyID, [FromRoute] int InternshipId, [FromRoute] int PositionId)
        {

                 await _service.InternshipPosition.DeleteInternshipPosition(CompanyID, InternshipId, PositionId);

            return NoContent();

        }


        [HttpPatch("{InternshipId}")]

        public async Task<IActionResult> UpdateInternship([FromRoute] string CompanyID, [FromRoute]int InternshipId, [FromForm] InternshipUpdateDto internshipUpdateDto) 
        {
           
                await _service.InternshipService.UpdateInternship(CompanyID, InternshipId, internshipUpdateDto);

            return NoContent();
        
        }

        [HttpPatch("{InternshipId}/positions{PositionId}")]

        public async Task<IActionResult> UpdateInternshipPosition([FromRoute] string CompanyID, [FromRoute] int InternshipId, [FromRoute] int PositionId, [FromBody] InternshipPositionUpdateDto internshipPositionUpdate)
        {
            
               
                 await _service.InternshipPosition.UpdateInternshipPosition(CompanyID, InternshipId, PositionId, internshipPositionUpdate);
              
             return NoContent();
        }

    }
}
