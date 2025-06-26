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

        private string SanitizeUserPrompt(string userPrompt)
        {
            // Handle common user typos/awkward phrasing
            if (userPrompt.Contains("what is miss me", StringComparison.OrdinalIgnoreCase))
            {
                return userPrompt.Replace("what is miss me", "what am I missing", StringComparison.OrdinalIgnoreCase);
            }
            if (userPrompt.Contains("what's miss me", StringComparison.OrdinalIgnoreCase))
            {
                return userPrompt.Replace("what's miss me", "what am I missing", StringComparison.OrdinalIgnoreCase);
            }
            if (userPrompt.Contains("what is missing for me", StringComparison.OrdinalIgnoreCase))
            {
                return userPrompt.Replace("what is missing for me", "what am I missing", StringComparison.OrdinalIgnoreCase);
            }
            if (userPrompt.Contains("what's missing for me", StringComparison.OrdinalIgnoreCase))
            {
                return userPrompt.Replace("what's missing for me", "what am I missing", StringComparison.OrdinalIgnoreCase);
            }
            if (userPrompt.Contains("what is missing in me", StringComparison.OrdinalIgnoreCase))
            {
                return userPrompt.Replace("what is missing in me", "what am I missing", StringComparison.OrdinalIgnoreCase);
            }
            if (userPrompt.Contains("what's missing in me", StringComparison.OrdinalIgnoreCase))
            {
                return userPrompt.Replace("what's missing in me", "what am I missing", StringComparison.OrdinalIgnoreCase);
            }
            return userPrompt;
        }

        public async IAsyncEnumerable<string> GenerateTextStreamAsync(
            string userPrompt,
            string model = "deepseek-r1:8b",
            string systemPrompt = null,
            string studentSkills = ""
        )
        {
            var sanitizedPrompt = SanitizeUserPrompt(userPrompt);
            var questionType = _questionClassificationService.ClassifyQuestion(sanitizedPrompt);

            var internshipNames = await GetInternshipNamesAsync();
            var scholarshipNames = await GetScholarshipNamesAsync();

            // Debug: Print the returned lists to the console
            Console.WriteLine($"Internship list count: {internshipNames?.Count ?? 0}");
            if (internshipNames != null)
            {
                foreach (var name in internshipNames)
                {
                    Console.WriteLine($"Internship: {name}");
                }
            }
            Console.WriteLine($"Scholarship list count: {scholarshipNames?.Count ?? 0}");
            if (scholarshipNames != null)
            {
                foreach (var name in scholarshipNames)
                {
                    Console.WriteLine($"Scholarship: {name}");
                }
            }

            string fullPrompt = BuildPrompt(sanitizedPrompt, questionType, studentSkills, internshipNames, scholarshipNames);

            // Directly return the custom message if there are no opportunities
            if (fullPrompt.StartsWith("ANSWER: There is no opportunity for you now"))
            {
                var jsonLine = $"{{\"response\": \"{fullPrompt.Replace("\"", "\\\"")}\"}}";
                yield return jsonLine;
                yield break;
            }

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
                var sanitizedPrompt = SanitizeUserPrompt(userPrompt);
                var questionType = _questionClassificationService.ClassifyQuestion(sanitizedPrompt);

                var internshipNames = await GetInternshipNamesAsync();
                var scholarshipNames = await GetScholarshipNamesAsync();

                // Debug: Print the returned lists to the console
                Console.WriteLine($"Internship list count: {internshipNames?.Count ?? 0}");
                if (internshipNames != null)
                {
                    foreach (var name in internshipNames)
                    {
                        Console.WriteLine($"Internship: {name}");
                    }
                }
                Console.WriteLine($"Scholarship list count: {scholarshipNames?.Count ?? 0}");
                if (scholarshipNames != null)
                {
                    foreach (var name in scholarshipNames)
                    {
                        Console.WriteLine($"Scholarship: {name}");
                    }
                }

                string fullPrompt = BuildPrompt(sanitizedPrompt, questionType, studentSkills, internshipNames, scholarshipNames);

                // Directly return the custom message if there are no opportunities
                if (fullPrompt.StartsWith("ANSWER: There is no opportunity for you now"))
                {
                    return fullPrompt;
                }

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

            var lowerPrompt = userPrompt.ToLowerInvariant();
            bool asksInternship = lowerPrompt.Contains("internship") || lowerPrompt.Contains("intership") || lowerPrompt.Contains("intrnship");
            bool asksScholarship = lowerPrompt.Contains("scholarship") || lowerPrompt.Contains("sholarship");
            bool asksOpportunity = lowerPrompt.Contains("opportunity") || lowerPrompt.Contains("opportunitiy") || lowerPrompt.Contains("opportunityies") || lowerPrompt.Contains("opportunities");

            // If user asks for opportunity and both lists are empty, return custom message
            if (asksOpportunity &&
                ( (internshipNames == null || internshipNames.Count == 0) && (scholarshipNames == null || scholarshipNames.Count == 0) ) )
            {
                return "ANSWER: There is no opportunity for you now, please try later.";
            }

            // Stricter logic: if user asks ONLY for scholarships and none exist, return custom message
            if (asksScholarship && !asksInternship && !asksOpportunity && (scholarshipNames == null || scholarshipNames.Count == 0))
            {
                return "ANSWER: There is no opportunity for you now, please try later.";
            }
            // If user asks ONLY for internships and none exist, return custom message
            if (asksInternship && !asksScholarship && !asksOpportunity && (internshipNames == null || internshipNames.Count == 0))
            {
                return "ANSWER: There is no opportunity for you now, please try later.";
            }
            // If user asks for both, and both lists are empty, return custom message
            if ((asksInternship && asksScholarship || asksOpportunity) &&
                (internshipNames == null || internshipNames.Count == 0) &&
                (scholarshipNames == null || scholarshipNames.Count == 0))
            {
                return "ANSWER: There is no opportunity for you now, please try later.";
            }

            // Dynamically add extra instruction to the prompt
            string extraInstruction = "";
            string formattedInternships = internshipNames != null ? string.Join("\n- ", internshipNames.Prepend("")) : "";
            string formattedScholarships = scholarshipNames != null ? string.Join("\n- ", scholarshipNames.Prepend("")) : "";

            if (asksScholarship && !asksInternship && !asksOpportunity)
            {
                extraInstruction = "Only recommend scholarships from the list below. Do not recommend internships.";
                formattedInternships = ""; // Hide internships from the prompt
            }
            else if (asksInternship && !asksScholarship && !asksOpportunity)
            {
                extraInstruction = "Only recommend internships from the list below. Do not recommend scholarships.";
                formattedScholarships = ""; // Hide scholarships from the prompt
            }
            // If asks both or asks opportunity, include both as before

            // Insert extraInstruction into the prompt template
            string promptTemplate = PromptTemplates.ConciseAnswer;
            if (!string.IsNullOrWhiteSpace(extraInstruction))
            {
                // Insert extraInstruction after the main instructions
                promptTemplate = promptTemplate.Replace("**Instructions:**", "**Instructions:**\n- " + extraInstruction);
            }

            return string.Format(
                promptTemplate,
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
