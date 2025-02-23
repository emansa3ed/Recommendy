using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public enum FeedbackType
	{
		Internship ,
		Scholarship
	}

	public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public string StudentId { get; set; }
		public int PostId { get; set; }
		public FeedbackType Type { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public Student Student { get; set; }
    }
}
