namespace Shared.DTO.Course
{
    public class CourseWithScoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CourseLink { get; set; }
        public string DifficultyLevel { get; set; }
        public string Platform { get; set; }
        public int Duration { get; set; }
        public string Skills { get; set; }
        public int Score { get; set; }
    }
} 