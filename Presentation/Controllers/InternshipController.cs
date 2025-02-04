using Contracts;
using Microsoft.AspNetCore.Authorization;
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
  
    public class InternshipController : ControllerBase
    {

        private readonly IServiceManager _service;
     
        public InternshipController( IServiceManager service, IFileRepository fileRepository) {

            _service = service; 
           
        
        }


        [HttpPost]
        public async Task<IActionResult> CreateInternship([FromForm] InternshipCreationDto internshipCreation)
        {

            try
            {
              
                var result = await _service.InternshipService.CreateInternship(internshipCreation);
              
                return Ok(result);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new ApiResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while creating the internship."+ ex.Message,
                    Data = -1
                });
            }
        }





        [HttpPost("Positions")]
        public async Task<IActionResult> CreatePosition( [FromBody]InternshipPositionDto internshipPositionDto)
        {
            try
            {
                var result =   await _service.InternshipPosition.CreateInternshipPosition(internshipPositionDto);
                return Ok(result);
            }
            catch (Exception ex) {

                return StatusCode(500, new ApiResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while creating the internshipPosition." + ex.Message,
                 
                });
            }

           
        }
        [HttpGet("Positions")]
        public async Task<IActionResult> GetPosition()
        {
            try
            {
                var result =  _service.PositionService.GetAllPositions(false);
                return Ok(result);

            }
            catch (Exception ex) {

                 return StatusCode(500, new ApiResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while Geting the Positions." + ex.Message,

                });
            }

        }

        [HttpGet("Company/{id:Guid}")]
        public async Task<IActionResult> GetInternshipByCompanyId(string id)
        {
            var internshipDto = await _service.InternshipService.GetInternshipsByCompanyId(id);

            if (internshipDto == null)
            {
                return NotFound();
            }

            return Ok(internshipDto);
        }


        [HttpDelete]

        public async Task<IActionResult> DeleteInternship(int Id)
        {

            try
            {

              var  result = await _service.InternshipService.DeleteInternship(Id, false);
                return Ok();
            }
            catch (Exception ex) {


                return StatusCode(500, new ApiResponse<int>
                {
                    Success = false,
                    Message = $"Failed to delete internship. {ex.Message} | Inner Exception: {ex.InnerException?.Message}",
                    Data = -1
                });

            }
          

        }

        [HttpDelete("Positions")]
        public  async Task<IActionResult> DeletePosition(int InternshipId, int PositionId)
        {

            try
            {
                 var result = await _service.InternshipPosition.DeleteInternshipPosition(InternshipId, PositionId);

                return Ok();
            }
            catch (Exception ex) {

                return StatusCode(500, new ApiResponse<int>
                {
                    Success = false,
                    Message = $"Failed to Delete InternshipPosition . {ex.Message} | Inner Exception: {ex.InnerException?.Message}",
                    Data = -1
                });


            }



        }
        [HttpPatch("{Id:int}")]

        public async Task<IActionResult> UpdateInternship(int Id, [FromForm] InternshipUpdateDto internshipUpdateDto) 
        {
            try
            {
               var result=  await _service.InternshipService.UpdateInternship(Id, internshipUpdateDto);
                if (result == true)
                    return Ok();
                else return BadRequest();
            }
            catch ( Exception ex){

                return StatusCode(500, new ApiResponse<int>
                {
                    Success = false,
                    Message = $"Failed to update internship. {ex.Message} | Inner Exception: {ex.InnerException?.Message}",
                    Data = -1
                });



            }

        
        }

        [HttpPatch("Position")]

        public async Task<IActionResult> UpdateInternshipPosition(int InternshipId, int PositionId , [FromBody] InternshipPositionUpdateDto internshipPositionUpdate)
        {
            try
            {
               
                var result = await _service.InternshipPosition.UpdateInternshipPosition(InternshipId, PositionId, internshipPositionUpdate);
              if(result == true) return Ok();   
              else return BadRequest();
            
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to edit internshipPosition. {ex.Message} | Inner Exception: {ex.InnerException?.Message}");

            }
        }

    }
}
