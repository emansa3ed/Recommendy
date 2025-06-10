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

namespace Service
{
    public class ProfileSuggestionService : IProfileSuggestionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly MyMemoryCache _memoryCache;

        public ProfileSuggestionService(IRepositoryManager repository, IMapper mapper, MyMemoryCache memoryCache)
        {
            _repository = repository;
            _mapper = mapper;
            _memoryCache = memoryCache;
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

                var expandedSkills = SkillOntology.ExpandSkills(studentSkills);

                Console.WriteLine(expandedSkills);

                var companies = await _repository.Company.GetAllCompaniesAsync(new CompanyParameters { PageSize = int.MaxValue }, false);
                var companySuggestions = companies
                    .Select(company => new CompanySuggestionDto
                    {
                        CompanyId = company.CompanyId,
                        Name = company.User.UserName,
                        Bio = company.User.Bio,
                        UrlPicture = company.User.UrlPicture,
                        CompanyUrl = company.CompanyUrl,
                        IsVerified = company.IsVerified,
                        MatchingSkills = GetMatchingSkills(company.User.Bio, expandedSkills),
                        MatchScore = CalculateMatchScore(company.User.Bio, expandedSkills)
                    })
                    .Where(c => c.MatchingSkills.Any())
                    .OrderByDescending(c => c.MatchScore)
                    .Take(5)
                    .ToList();

                var universities = await _repository.university.GetAllUniversitiesAsync(new UniversityParameters { PageSize = int.MaxValue }, false);
                var universitySuggestions = universities
                    .Select(university => new UniversitySuggestionDto
                    {
                        UniversityId = university.UniversityId,
                        Name = university.User.UserName,
                        Bio = university.User.Bio,
                        UrlPicture = university.User.UrlPicture,
                        UniversityUrl = university.UniversityUrl,
                        IsVerified = university.IsVerified,
                        MatchingSkills = GetMatchingSkills(university.User.Bio, expandedSkills),
                        MatchScore = CalculateMatchScore(university.User.Bio, expandedSkills)
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

        private List<string> GetMatchingSkills(string bio, IEnumerable<string> expandedSkills)
        {
            if (string.IsNullOrWhiteSpace(bio))
                return new List<string>();

            var bioLower = bio.ToLower();
            return expandedSkills
                .Where(skill => bioLower.Contains(skill))
                .ToList();
        }

        private double CalculateMatchScore(string bio, IEnumerable<string> expandedSkills)
        {
            if (string.IsNullOrWhiteSpace(bio) || !expandedSkills.Any())
                return 0;

            var matchingSkills = GetMatchingSkills(bio, expandedSkills);
            return (double)matchingSkills.Count / expandedSkills.Count();
        }
    }
} 