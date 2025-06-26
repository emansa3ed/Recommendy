using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.DTO.Internship;
using Shared.DTO.Scholaship;
using Shared.DTO.Ollama;

namespace Service.Contracts
{
    public interface IOllamaService
    {
        Task<string> GenerateTextAsync(
            string userPrompt,
            string model = "deepseek-r1:8b",
            bool stream = false,
            string systemPrompt = null,
            string studentSkills = "");
        Task<string> RecommendedOpportunities(
            string userPrompt,
            string model = "deepseek-r1:8b",
            bool stream = false,
            string systemPrompt = null,
            string promptType = "recommendation");

        IAsyncEnumerable<string> GenerateTextStreamAsync(string userPrompt, string model = "deepseek-r1:8b",
            string systemPrompt = null, string studentSkills = "");


    }
} 