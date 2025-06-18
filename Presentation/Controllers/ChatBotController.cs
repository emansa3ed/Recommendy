using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Ollama;
using Shared.RequestFeatures;
using System.Linq;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OllamaController : ControllerBase
    {
        private readonly IOllamaService _ollamaService;
        private readonly IServiceManager _service;

        public OllamaController(IOllamaService ollamaService, IServiceManager service)
        {
            _ollamaService = ollamaService;
            _service = service;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateText([FromBody] GenerateTextRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
            {
                return BadRequest("Prompt cannot be empty");
            }

            // Get logged-in user
            var username = User.Identity.Name;
            var user = await _service.UserService.GetDetailsByUserName(username);
            var student = _service.StudentService.GetStudent(user.Id, trackChanges: false);
            var studentSkills = student.Skills ?? string.Empty;

            

            var response = await _ollamaService.GenerateTextAsync(
                request.Prompt,
                request.Model,
                request.Stream,
                request.SystemPrompt,
                request.PromptType,
                studentSkills    
            );
            return Ok(new { response });
        }

    
    }


   
} 