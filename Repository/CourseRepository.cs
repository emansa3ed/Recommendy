using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
namespace Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly RepositoryContext _context;
        public CourseRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByKeywordsAsync(List<string> keywords)
        {
            if (keywords == null || !keywords.Any())
                return new List<Course>();

            return await _context.Courses
                .Where(c => keywords.Any(k => (c.Skills != null && c.Skills.Contains(k)) || (c.Description != null && c.Description.Contains(k))))
                .ToListAsync();
        }

        public async Task AddCoursesAsync(IEnumerable<Course> courses)
        {
            await _context.Courses.AddRangeAsync(courses);
            await _context.SaveChangesAsync();
        }
    }
} 