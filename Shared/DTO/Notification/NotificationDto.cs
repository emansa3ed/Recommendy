using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Feedback
{
	public record NotificationDto
	{
		public string ReceiverID { get; set; }
		public string ActorID { get; set; }
		public string Content { get; set; }
		public bool IsRead { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
