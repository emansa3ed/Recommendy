using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Notification
{
    public record NotificationCreationDto
	{
		[Required(ErrorMessage = "ReceiverID Id is required")]
		public string ReceiverID { get; set; }
		
		[Required(ErrorMessage = "ActorID Id is required")]
		public string ActorID { get; set; }

		[Required(ErrorMessage = "Content is required")]
		[StringLength(200, ErrorMessage = "Content cannot be longer than 200 characters")]
		public string Content { get; set; }
	}
}