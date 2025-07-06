﻿using Entities.Exceptions;
using Entities.GeneralResponse;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/User/{UserId}")]
    [ApiController]
    [Authorize]
    public  class UserController : ControllerBase
    {
        private readonly IServiceManager _service;
        public UserController(IServiceManager service) 
        { 
            _service = service;
        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword( [FromRoute] string UserId ,  [FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

			var UserName = User.Identity?.Name;

			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (user.Id != UserId)
				return BadRequest(new ApiResponse<Internship> { Success = false, Message = "UserId don't match with authorized one", Data = null });

			await _service.UserService.ChangePasswordAsync(UserId, changePasswordDto);
            return NoContent();
        }

        [HttpPost("upload-profile-picture")]

        public async Task<IActionResult> UploadProfilePicture([FromRoute] string UserId ,IFormFile file)
        {
			var UserName = User.Identity?.Name;

			var user = await _service.UserService.GetDetailsByUserName(UserName);

			if (user.Id != UserId)
				return BadRequest(new ApiResponse<Internship> { Success = false, Message = "UserId don't match with authorized one", Data = null });
			var imageUrl = await _service.UserService.UploadProfilePictureAsync(file, UserId);
            return NoContent();
           
        }
    }
}
