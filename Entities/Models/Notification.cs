using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	public class Notification
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("User")]
		public string ReceiverID { get; set; }
		public string ActorID { get; set; }
		public string Content { get; set; }
		public bool IsRead { get; set; }
		public DateTime CreatedAt { get; set; }
		public User User { get; set; }

	}
}
