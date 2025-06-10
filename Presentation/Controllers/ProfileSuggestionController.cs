using Entities.GeneralResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Student;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class ProfileSuggestionController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ProfileSuggestionController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("suggestions")]
        public async Task<IActionResult> GetProfileSuggestions()
        {
            var username = User.Identity.Name;
            var user = await _service.UserService.GetDetailsByUserName(username);
            var student = _service.StudentService.GetStudent(user.Id, trackChanges: false);

            var suggestions = await _service.ProfileSuggestionService.GetProfileSuggestionsAsync(student.StudentId);

            return Ok(new ApiResponse<ProfileSuggestionDto>
            {
                Success = true,
                Message = "Profile suggestions retrieved successfully.",
                Data = suggestions
            });
        }
    }
} 