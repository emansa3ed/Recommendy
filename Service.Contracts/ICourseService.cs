using Entities.Models;
using Shared.DTO.Course;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICourseService
    {
        Task<List<CourseWithScoreDto>> GetCoursesForInternshipAsync(int internshipId);
        Task<List<CourseWithScoreDto>> GetCoursesForScholarshipAsync(int scholarshipId);
    }
} 