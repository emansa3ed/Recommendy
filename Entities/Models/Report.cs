using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public enum ReportType
    {
        Internship,
        Scholarship
    }
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TypeId { get; set; }
        public ReportType Type { get; set; }
        public string Content { get; set; }
        public string Status { get; set; } = "UnReviewed";/// "UnReviewed" ,,,,,,,"Reviewed"
        public DateTime CreatedAt { get; set; } = DateTime.Now;

  
    }
}
