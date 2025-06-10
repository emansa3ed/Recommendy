using System.Collections.Generic;

namespace Shared.DTO.Student
{
    public class ProfileSuggestionDto
    {
        public List<CompanySuggestionDto> CompanySuggestions { get; set; }
        public List<UniversitySuggestionDto> UniversitySuggestions { get; set; }
    }

    public class CompanySuggestionDto
    {
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string UrlPicture { get; set; }
        public string CompanyUrl { get; set; }
        public bool IsVerified { get; set; }
        public List<string> MatchingSkills { get; set; }
        public double MatchScore { get; set; }
    }

    public class UniversitySuggestionDto
    {
        public string UniversityId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string UrlPicture { get; set; }
        public string UniversityUrl { get; set; }
        public bool IsVerified { get; set; }
        public List<string> MatchingSkills { get; set; }
        public double MatchScore { get; set; }
    }

    public class InternshipSuggestionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Requirements { get; set; }
    }

    public class ScholarshipSuggestionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
    }
} 