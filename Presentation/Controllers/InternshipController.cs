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
using Shared.RequestFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Shared.DTO.Report;


namespace Presentation.Controllers
{
    [Route("api/Companies/{CompanyID}/[controller]")]
    [ApiController]
   

    public class InternshipController : ControllerBase
    {

        private readonly IServiceManager _service;
     
        public InternshipController( IServiceManager service) {

            _service = service; 
           
        
        }


        [HttpPost]
        [Authorize(Roles = "Company")]
        [Authorize(Policy = "VerifiedOrganization")]
        public async Task<ActionResult<ApiResponse<Internship>>> CreateInternship( [FromRoute] string CompanyID ,[FromForm] InternshipCreationDto internshipCreation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var UserName = User.Identity?.Name;

            var user = await _service.UserService.GetDetailsByUserName(UserName);

            if (user.Id != CompanyID)
                return BadRequest(new ApiResponse<Internship> { Success = false,Message = "CompanyID don't match with authorized one",Data=null });

			var result = await _service.InternshipService.CreateInternship(CompanyID, internshipCreation);
              
                return Ok(  new ApiResponse<Internship>
                {
                    Success = true,
                    Message = "Internship created successfully.",
                    Data = result
                });      
        }



        

        [HttpPost("{InternshipId}/Positions")]
        [Authorize(Roles = "Company")]
        [Authorize(Policy = "VerifiedOrganization")]
        public async Task<IActionResult> CreatePosition([FromRoute] string CompanyID, [FromRoute] int InternshipId, [FromBody]InternshipPositionDto internshipPositionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

			var UserName = User.Identity?.Name;

			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (user.Id != CompanyID)
				return BadRequest(new ApiResponse<Internship> { Success = false, Message = "CompanyID don't match with authorized one", Data = null });

			var result =   await _service.InternshipPosition.CreateInternshipPosition(CompanyID, InternshipId, internshipPositionDto);


            return Ok(new ApiResponse<InternshipPosition>
            {
                Success = true,
                Message = "InternshipPosition created successfully.",
                Data = result
            });


        }
        [HttpGet("StoredPositions")]
        [Authorize(Roles = "Company")]
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
        [Authorize]
        public async Task<ActionResult<ApiResponse<PagedList<InternshipDto>>>> GetInternshipsByCompanyId([FromRoute] string CompanyID, [FromQuery] InternshipParameters internshipParameters)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var internshipDto = await _service.InternshipService.GetInternshipsByCompanyId(CompanyID, internshipParameters);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(internshipDto.MetaData));


            return Ok(new ApiResponse<PagedList<InternshipDto>> { Success = true, Message = "Fetch success", Data = internshipDto }); 


        }


        [HttpDelete("{InternshipId}")]
        [Authorize(Roles = "Company")]
        [Authorize(Policy = "VerifiedOrganization")]
        public async Task<IActionResult> DeleteInternship([FromRoute] string CompanyID, [FromRoute] int InternshipId)
        {

			var UserName = User.Identity?.Name;

			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (user.Id != CompanyID)
				return BadRequest(new ApiResponse<Internship> { Success = false, Message = "CompanyID don't match with authorized one", Data = null });

			await _service.InternshipService.DeleteInternship(CompanyID, InternshipId, false);

                return NoContent();
           
          

        }

        [HttpDelete("{InternshipId}/positions/{PositionId}")]
        [Authorize(Roles = "Company")]
        [Authorize(Policy = "VerifiedOrganization")]
        public  async Task<IActionResult> DeletePosition([FromRoute] string CompanyID, [FromRoute] int InternshipId, [FromRoute] int PositionId)
        {
		
            var UserName = User.Identity?.Name;

			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (user.Id != CompanyID)
				return BadRequest(new ApiResponse<Internship> { Success = false, Message = "CompanyID don't match with authorized one", Data = null });

			await _service.InternshipPosition.DeleteInternshipPosition(CompanyID, InternshipId, PositionId);

            return NoContent();

        }


        [HttpPatch("{InternshipId}")]
        [Authorize(Roles = "Company")]
        [Authorize(Policy = "VerifiedOrganization")]

        public async Task<IActionResult> UpdateInternship([FromRoute] string CompanyID, [FromRoute]int InternshipId, [FromForm] InternshipUpdateDto internshipUpdateDto) 
        {

			var UserName = User.Identity?.Name;

			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (user.Id != CompanyID)
				return BadRequest(new ApiResponse<Internship> { Success = false, Message = "CompanyID don't match with authorized one", Data = null });

			await _service.InternshipService.UpdateInternship(CompanyID, InternshipId, internshipUpdateDto);

            return NoContent();
        
        }

        [HttpPatch("{InternshipId}/positions{PositionId}")]
        [Authorize(Roles = "Company")]
        [Authorize(Policy = "VerifiedOrganization")]

        public async Task<IActionResult> UpdateInternshipPosition([FromRoute] string CompanyID, [FromRoute] int InternshipId, [FromRoute] int PositionId, [FromBody] InternshipPositionUpdateDto internshipPositionUpdate)
        {

			var UserName = User.Identity?.Name;

			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (user.Id != CompanyID)
				return BadRequest(new ApiResponse<Internship> { Success = false, Message = "CompanyID don't match with authorized one", Data = null });

			await _service.InternshipPosition.UpdateInternshipPosition(CompanyID, InternshipId, PositionId, internshipPositionUpdate);
              
            return NoContent();
        }

    }
}
