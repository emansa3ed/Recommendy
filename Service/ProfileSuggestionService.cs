using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.Extensions.Caching.Memory;
using Service.Contracts;
using Shared.DTO.Student;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Service.Ontology;
using System.Text;
using Shared.RequestFeatures;

namespace Service
{
    public class ProfileSuggestionService : IProfileSuggestionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly MyMemoryCache _memoryCache;
        private readonly IServiceManager _serviceManager;

        public ProfileSuggestionService(IRepositoryManager repository, IMapper mapper, MyMemoryCache memoryCache , IServiceManager serviceManager)
        {
            _repository = repository;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _serviceManager = serviceManager;
        }

        public async Task<ProfileSuggestionDto> GetProfileSuggestionsAsync(string studentId)
        {
            var cacheKey = $"ProfileSuggestions_{studentId}";

            if (!_memoryCache.Cache.TryGetValue(cacheKey, out ProfileSuggestionDto suggestions))
            {
                var student = _repository.Student.GetStudent(studentId, trackChanges: false);
                if (student == null)
                    throw new StudentNotFoundException(studentId);

                if (string.IsNullOrWhiteSpace(student.Skills))
                    return new ProfileSuggestionDto
                    {
                        CompanySuggestions = new List<CompanySuggestionDto>(),
                        UniversitySuggestions = new List<UniversitySuggestionDto>()
                    };

                var studentSkills = student.Skills.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(s => s.ToLower())
                    .ToList();

                var expandedSkills = _serviceManager.SkillOntology.ExpandSkills(studentSkills);


                var companies = await _repository.Company.GetAllCompaniesAsync(new CompanyParameters { PageSize = int.MaxValue }, false);
                var companySuggestions = companies
                    .Select(company => {

                        var internships = _repository.Intership.GetInternshipsByCompanyId(company.CompanyId, new InternshipParameters { PageSize = int.MaxValue }, false).Result;
                        
                       
                        var combinedText = new StringBuilder();
                        combinedText.AppendLine(company.User.Bio);
                        
                        foreach (var internship in internships)
                        {
                            combinedText.AppendLine($"Internship: {internship.Name}");
                            combinedText.AppendLine($"Description: {internship.Description}");
                            
                            if (internship.InternshipPositions != null)
                            {
                                foreach (var position in internship.InternshipPositions)
                                {
                                    combinedText.AppendLine($"Position Requirements: {position.Requirements}");
                                }
                            }
                        }

                        var matchingSkills = GetMatchingSkills(combinedText.ToString(), expandedSkills);
                        var matchScore = CalculateMatchScore(matchingSkills, expandedSkills);

                        return new CompanySuggestionDto
                        {
                            CompanyId = company.CompanyId,
                            Name = company.User.UserName,
                            Bio = company.User.Bio,
                            UrlPicture = company.User.UrlPicture,
                            CompanyUrl = company.CompanyUrl,
                            IsVerified = company.IsVerified,
                            MatchingSkills = matchingSkills,
                            MatchScore = matchScore
                        };
                    })
                    .Where(c => c.MatchingSkills.Any())
                    .OrderByDescending(c => c.MatchScore)
                    .Take(5)
                    .ToList();


                var universities = await _repository.university.GetAllUniversitiesAsync(new UniversityParameters { PageSize = int.MaxValue }, false);
                var universitySuggestions = universities
                    .Select(university => {

                     
                        var scholarships = _repository.Scholarship.GetAllScholarshipsAsync(university.UniversityId, new ScholarshipsParameters { PageSize = int.MaxValue }, false).Result;
                        
                        var combinedText = new StringBuilder();

                        combinedText.AppendLine(university.User.Bio);
                        
                        foreach (var scholarship in scholarships)
                        {
                            combinedText.AppendLine($"Scholarship: {scholarship.Name}");
                            combinedText.AppendLine($"Description: {scholarship.Description}");
                            combinedText.AppendLine($"Requirements: {scholarship.Requirements}");
                        }

                        var matchingSkills = GetMatchingSkills(combinedText.ToString(), expandedSkills);
                        var matchScore = CalculateMatchScore(matchingSkills, expandedSkills);

                        return new UniversitySuggestionDto
                        {
                            UniversityId = university.UniversityId,
                            Name = university.User.UserName,
                            Bio = university.User.Bio,
                            UrlPicture = university.User.UrlPicture,
                            UniversityUrl = university.UniversityUrl,
                            IsVerified = university.IsVerified,
                            MatchingSkills = matchingSkills,
                            MatchScore = matchScore
                        };
                    })
                    .Where(u => u.MatchingSkills.Any())
                    .OrderByDescending(u => u.MatchScore)
                    .Take(5)
                    .ToList();

                suggestions = new ProfileSuggestionDto
                {
                    CompanySuggestions = companySuggestions,
                    UniversitySuggestions = universitySuggestions
                };

                var jsonSize = JsonSerializer.SerializeToUtf8Bytes(suggestions).Length;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSize(jsonSize)
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Cache.Set(cacheKey, suggestions, cacheEntryOptions);
            }

            return suggestions;
        }

        private List<string> GetMatchingSkills(string text, IEnumerable<string> expandedSkills)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            var textLower = text.ToLower();
            return expandedSkills
                .Where(skill => textLower.Contains(skill))
                .ToList();
        }

        private double CalculateMatchScore(List<string> matchingSkills, IEnumerable<string> expandedSkills)
        {
            if (!matchingSkills.Any() || !expandedSkills.Any())
                return 0;

            return (double)matchingSkills.Count / expandedSkills.Count();
        }
    }
} 