using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TypeId { get; set; }
        public char Type { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

  
    }
}
