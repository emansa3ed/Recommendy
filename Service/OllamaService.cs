using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
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

        public OllamaService(HttpClient httpClient, IRepositoryManager repositoryManager)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
            _repositoryManager = repositoryManager;
        }

        public async Task<string> GenerateTextAsync(
            string userPrompt,
            string model = "deepseek-r1:8b",
            bool stream = false,
            string systemPrompt = null,
            string promptType = "recommendation",
            string studentSkills = ""
            
            )
        {
            // Fetch all internships (names only)
            var internshipParameters = new InternshipParameters { PageNumber = 1, PageSize = 1000 };
            var internshipsPaged = await _repositoryManager.Intership.GetAllInternshipsAsync(internshipParameters, trackChanges: false);
            List<string> internshipNames  = internshipsPaged.Select(i => i.Name).ToList();

            // Fetch all scholarships (names only)
            var scholarshipParameters = new ScholarshipsParameters { PageNumber = 1, PageSize = 1000 };
            var scholarshipsPaged = await  _repositoryManager.Scholarship.GetAllScholarshipsAsync(scholarshipParameters, trackChanges: false);
            List<string> scholarshipNames = scholarshipsPaged.Select(s => s.Name).ToList();



            const string RecommendationStaticPart =  @"
You are Recommendy, a smart recommendation system.

Student skills:
{0}

Internship opportunities:
{1}

Scholarship opportunities:
{2}

Based on the student's skills, recommend the most suitable internship and scholarship.

Respond clearly and directly. Do not include explanations or internal thoughts. Start your answer with 'ANSWER:' only.

Question: ";


            const string ExpertAdviceStaticPart =  @"
You are Recommendy, an expert career advisor.

Student skills: {0}

Based on these skills, give precise and actionable advice tailored to the student. Avoid explanations or internal thoughts. Answer directly and clearly.

Start your response with 'ANSWER:' only.

Question: ";


            string fullPrompt;
            if (promptType?.ToLower() == "expert")
            {
                fullPrompt = string.Format(ExpertAdviceStaticPart, studentSkills) + userPrompt.Trim();
            }
            else
            {
                string formattedInternships = internshipNames != null ? string.Join("\n- ", internshipNames.Prepend("").ToArray()) : "";
                string formattedScholarships = scholarshipNames != null ? string.Join("\n- ", scholarshipNames.Prepend("").ToArray()) : "";
                fullPrompt = string.Format(
                    RecommendationStaticPart,
                    studentSkills,
                    formattedInternships,
                    formattedScholarships
                ) + userPrompt.Trim();
            }

            var request = new
            {
                model = model,
                prompt = fullPrompt,
                stream = stream,
                system = systemPrompt
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("/api/generate", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Ollama raw response: " + responseContent);
            var responseObject = JsonSerializer.Deserialize<OllamaResponse>(responseContent);

            var answer = responseObject?.Response ?? string.Empty;
            answer = Regex.Replace(answer, @"<think>[\s\S]*?</think>", "", RegexOptions.IgnoreCase).Trim();
            return answer;
        }

       
    }

   
}
