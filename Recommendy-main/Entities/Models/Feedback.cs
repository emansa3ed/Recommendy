using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public int TypeId { get; set; }
        public char Type { get; set; }
        public string StudentId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public int TotalRating { get; set; }
        public DateTime CreatedAt { get; set; }
       
    }
}
