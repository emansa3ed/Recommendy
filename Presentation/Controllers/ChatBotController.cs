using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Ollama;
using Shared.RequestFeatures;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Student")]
    public class ChatBotController : ControllerBase
    {
        private readonly IOllamaService _ollamaService;
        private readonly IServiceManager _service;

        public ChatBotController(IOllamaService ollamaService, IServiceManager service)
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

        [HttpPost("generate/stream")]
        public async Task GenerateTextStream([FromBody] GenerateTextRequest request)
        {
            Response.ContentType = "text/plain";
            try
            {
                var username = User.Identity.Name;
                var user = await _service.UserService.GetDetailsByUserName(username);
                var student = _service.StudentService.GetStudent(user.Id, trackChanges: false);
                var studentSkills = student.Skills ?? string.Empty;

                await foreach (var chunk in _ollamaService.GenerateTextStreamAsync(
                    request.Prompt,
                    request.Model,
                    request.SystemPrompt,
                    request.PromptType,
                    studentSkills))
                {
                    try
                    {
                        var jsonDoc = JsonDocument.Parse(chunk);
                        if (jsonDoc.RootElement.TryGetProperty("response", out var responseProp))
                        {
                            var text = responseProp.GetString();
                            if (!string.IsNullOrWhiteSpace(text))
                            {
                                await Response.WriteAsync(text);
                                await Response.Body.FlushAsync();
                            }
                        }
                    }
                    catch (JsonException)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                await Response.WriteAsync("Sorry, the AI is currently unavailable. Please try again later.");
            }
        }

    }

}