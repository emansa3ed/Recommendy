using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading;
using Service.Contracts;
using Shared.DTO.Ollama;
using Contracts;
using Shared.RequestFeatures;

namespace Service
{
    public class OllamaService : IOllamaService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:11434";
        private readonly IRepositoryManager _repositoryManager;
        private readonly IQuestionClassificationService _questionClassificationService;

        public OllamaService(HttpClient httpClient, IRepositoryManager repositoryManager, IQuestionClassificationService questionClassificationService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
            _repositoryManager = repositoryManager;
            _questionClassificationService = questionClassificationService;
        }

        public async IAsyncEnumerable<string> GenerateTextStreamAsync(
            string userPrompt,
            string model = "deepseek-r1:8b",
            string systemPrompt = null,
            string studentSkills = ""
        )
        {
            var questionType = _questionClassificationService.ClassifyQuestion(userPrompt);

            var internshipNames = await GetInternshipNamesAsync();
            var scholarshipNames = await GetScholarshipNamesAsync();

            string fullPrompt = BuildPrompt(userPrompt, questionType, studentSkills, internshipNames, scholarshipNames);

            var request = new
            {
                model = model,
                prompt = fullPrompt,
                stream = true,
                system = systemPrompt
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/generate") { Content = content };

            using var response = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    yield return line;
                }
            }
        }

        public async Task<string> GenerateTextAsync(
            string userPrompt,
            string model = "deepseek-r1:8b",
            bool stream = false,
            string systemPrompt = null,
            string studentSkills = ""
        )
        {
            try
            {
                var questionType = _questionClassificationService.ClassifyQuestion(userPrompt);

                var internshipNames = await GetInternshipNamesAsync();
                var scholarshipNames = await GetScholarshipNamesAsync();

                string fullPrompt = BuildPrompt(userPrompt, questionType, studentSkills, internshipNames, scholarshipNames);

                var request = new
                {
                    model = model,
                    prompt = fullPrompt,
                    stream = true, // force stream to handle all models like gemma
                    system = systemPrompt
                };

                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/generate") { Content = content };

                using var response = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                using var streamResponse = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(streamResponse);

                var completeAnswer = new StringBuilder();

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        try
                        {
                            var jsonDoc = JsonDocument.Parse(line);
                            if (jsonDoc.RootElement.TryGetProperty("response", out var responseProp))
                            {
                                completeAnswer.Append(responseProp.GetString());
                            }
                        }
                        catch (JsonException)
                        {
                            // skip invalid json lines
                        }
                    }
                }

                return completeAnswer.ToString().Trim();
            }
            catch (Exception ex)
            {
                // log ex if needed
                return "Sorry, I'm having trouble answering right now. Please try again later or contact support if the issue persists.";
            }
        }

        // Reusable prompt builder
        private string BuildPrompt(string userPrompt, QuestionType questionType, string studentSkills, List<string> internshipNames, List<string> scholarshipNames)
        {
            if (questionType == QuestionType.Irrelevant)
            {
                return PromptTemplates.IrrelevantQuestion + userPrompt.Trim();
            }

            // questionType is Relevant, so use the concise prompt.
            string formattedInternships = internshipNames != null ? string.Join("\n- ", internshipNames.Prepend("")) : "";
            string formattedScholarships = scholarshipNames != null ? string.Join("\n- ", scholarshipNames.Prepend("")) : "";

            return string.Format(
                PromptTemplates.ConciseAnswer,
                userPrompt.Trim(),
                studentSkills,
                formattedInternships,
                formattedScholarships
            );
        }

        // Fetch internships
        private async Task<List<string>> GetInternshipNamesAsync()
        {
            var parameters = new InternshipParameters { PageNumber = 1, PageSize = 1000 };
            var data = await _repositoryManager.Intership.GetAllInternshipsAsync(parameters, trackChanges: false);
            return data.Select(i => i.Name).ToList();
        }

        // Fetch scholarships
        private async Task<List<string>> GetScholarshipNamesAsync()
        {
            var parameters = new ScholarshipsParameters { PageNumber = 1, PageSize = 1000 };
            var data = await _repositoryManager.Scholarship.GetAllScholarshipsAsync(parameters, trackChanges: false);
            return data.Select(s => s.Name).ToList();
        }

        public async Task<string> RecommendedOpportunities(string userPrompt, string model = "deepseek-r1:8b", bool stream = false, string systemPrompt = null, string promptType = "recommendation")
        {
            try
            {
                var request = new
                {
                    model = model,
                    prompt = userPrompt,
                    stream = true, // force stream to handle all models like gemma
                    system = systemPrompt
                };

                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/generate") { Content = content };

                var response = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                var streamResponse = await response.Content.ReadAsStreamAsync();
                var reader = new StreamReader(streamResponse);

                var completeAnswer = new StringBuilder();

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        try
                        {
                            var jsonDoc = JsonDocument.Parse(line);
                            if (jsonDoc.RootElement.TryGetProperty("response", out var responseProp))
                            {
                                completeAnswer.Append(responseProp.GetString());
                            }
                        }
                        catch (JsonException)
                        {
                            // skip invalid json lines
                        }
                    }
                }

                return completeAnswer.ToString().Trim();
            }
            catch (Exception ex)
            {
                // log ex if needed
                return "Sorry, I'm having trouble answering right now. Please try again later or contact support if the issue persists.";
            }
        }
	}

 
    
}
