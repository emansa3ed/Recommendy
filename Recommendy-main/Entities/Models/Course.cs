using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CourseLink { get; set; }
        public string DifficultyLevel { get; set; } // Enum
        public string Platform { get; set; }
        public int Duration { get; set; }
        public string Skills { get; set; }
    }
}
