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
        private readonly IServiceManager _service;

        public ChatBotController(IServiceManager service)
        {
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

            var response = await _service.OllamaService.GenerateTextAsync(
                request.Prompt,
                request.Model,
                request.Stream,
                request.SystemPrompt,
                studentSkills
            );

            var questionType = _service.QuestionClassificationService.ClassifyQuestion(request.Prompt);

            return Ok(new { 
                response,
                questionType = questionType.ToString(),
                isRelevant = questionType != QuestionType.Irrelevant
            });
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

                await foreach (var chunk in _service.OllamaService.GenerateTextStreamAsync(
                    request.Prompt,
                    request.Model,
                    request.SystemPrompt,
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

        [HttpPost("classify")]
        public IActionResult ClassifyQuestion([FromBody] GenerateTextRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
            {
                return BadRequest("Prompt cannot be empty");
            }

            var questionType = _service.QuestionClassificationService.ClassifyQuestion(request.Prompt);
            
            return Ok(new { 
                questionType = questionType.ToString(),
                isRelevant = questionType != QuestionType.Irrelevant,
                message = questionType == QuestionType.Irrelevant 
                    ? "This question is not related to career advice, internships, or education. Please ask questions about career guidance, internships, scholarships, or educational planning."
                    : "This question is relevant to career and educational topics."
            });
        }

    }

}