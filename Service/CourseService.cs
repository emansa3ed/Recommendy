using Entities.Models;
using Service.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.DTO.Course;
using Contracts;

public class CourseService : ICourseService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ISkillOntology _skillOntology;
    public CourseService(IRepositoryManager repositoryManager, ISkillOntology skillOntology)
    {
        _repositoryManager = repositoryManager;
        _skillOntology = skillOntology;
    }

    public async Task<List<CourseWithScoreDto>> GetCoursesForInternshipAsync(int internshipId)
    {
        bool s = false;
        var positions = await _repositoryManager.InternshipPosition.GetAllByInternshipIdAsync(internshipId);
        var internship = await _repositoryManager.Intership.InternshipById(internshipId, s);
        var requirements = positions.SelectMany(p => (p.Requirements ?? "").Split(',', System.StringSplitOptions.RemoveEmptyEntries)).Select(k => k.Trim().ToLower()).Where(k => !string.IsNullOrWhiteSpace(k)).ToList();
        var name = internship?.Name ?? string.Empty;
        var description = internship?.Description ?? string.Empty;
        return await GetScoredCoursesByContextAsync(requirements, name, description);
    }

    public async Task<List<CourseWithScoreDto>> GetCoursesForScholarshipAsync(int scholarshipId)
    {
        var scholarship = await _repositoryManager.Scholarship.GetByIdAsync(scholarshipId);
        if (scholarship == null)
            return new List<CourseWithScoreDto>();
        var requirements = (scholarship.Requirements ?? "").Split(',', System.StringSplitOptions.RemoveEmptyEntries).Select(k => k.Trim().ToLower()).Where(k => !string.IsNullOrWhiteSpace(k)).ToList();
        var name = scholarship.Name ?? string.Empty;
        var description = scholarship.Description ?? string.Empty;
        return await GetScoredCoursesByContextAsync(requirements, name, description);
    }

    private async Task<List<CourseWithScoreDto>> GetScoredCoursesByContextAsync(IEnumerable<string> requirements, string name, string description)
    {
        var nameKeywords = (name ?? "").Split(' ', System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries).Select(k => k.ToLower());
        var descKeywords = (description ?? "").Split(' ', System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries).Select(k => k.ToLower());
        var allKeywords = requirements.Concat(nameKeywords).Concat(descKeywords).Distinct().ToList();
        var expandedSkills = _skillOntology.ExpandSkills(allKeywords);
        var allCourses = await _repositoryManager.CourseRepository.GetAllCoursesAsync();
        var scoredCourses = new List<(Course course, int score)>();
        foreach (var course in allCourses)
        {
            int score = 0;
            var courseSkills = (course.Skills ?? "").ToLower();
            var courseDescription = (course.Description ?? "").ToLower();
            var courseName = (course.Name ?? "").ToLower();
            foreach (var skill in expandedSkills)
            {
                if (requirements.Any(k => k.Equals(skill, System.StringComparison.OrdinalIgnoreCase)) && (courseSkills.Contains(skill) || courseDescription.Contains(skill)))
                    score += 3; // Requirement match
                if (name.ToLower().Contains(skill) && (courseName.Contains(skill) || courseSkills.Contains(skill)))
                    score += 2; // Name match
                if (description.ToLower().Contains(skill) && (courseDescription.Contains(skill) || courseSkills.Contains(skill)))
                    score += 1; // Description match
            }
            if (score > 0)
                scoredCourses.Add((course, score));
        }
        return scoredCourses
            .OrderByDescending(cs => cs.score)
            .Take(5)
            .Select(cs => new CourseWithScoreDto
            {
                Id = cs.course.Id,
                Name = cs.course.Name,
                Description = cs.course.Description,
                CourseLink = cs.course.CourseLink,
                DifficultyLevel = cs.course.DifficultyLevel,
                Platform = cs.course.Platform,
                Duration = cs.course.Duration,
                Skills = cs.course.Skills,
                Score = cs.score
            })
            .ToList();
    }
} 