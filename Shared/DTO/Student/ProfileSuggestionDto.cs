using System.Collections.Generic;

namespace Shared.DTO.Student
{
    public record ProfileSuggestionDto
    {
        public List<CompanySuggestionDto> CompanySuggestions { get; init; }
        public List<UniversitySuggestionDto> UniversitySuggestions { get; init; }
    }

    public record CompanySuggestionDto
    {
        public string CompanyId { get; init; }
        public string Name { get; init; }
        public string Bio { get; init; }
        public string UrlPicture { get; init; }
        public string CompanyUrl { get; init; }
        public bool IsVerified { get; init; }
        public double MatchScore { get; init; }
        public List<string> MatchingSkills { get; init; }
    }

    public record UniversitySuggestionDto
    {
        public string UniversityId { get; init; }
        public string Name { get; init; }
        public string Bio { get; init; }
        public string UrlPicture { get; init; }
        public string UniversityUrl { get; init; }
        public bool IsVerified { get; init; }
        public double MatchScore { get; init; }
        public List<string> MatchingSkills { get; init; }
    }
} 