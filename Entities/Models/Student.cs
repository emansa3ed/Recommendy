using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Student 
    {
        [Key]
        public string StudentId { get; set; }
        public string ? UrlResume { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
	}

}
