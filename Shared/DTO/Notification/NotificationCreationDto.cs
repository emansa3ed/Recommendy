using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Notification
{
	public enum NotificationType
	{
		SaveOpportunity,
		CreateFeedBack
	}

	public record NotificationCreationDto
	{
		[Required(ErrorMessage = "ReceiverID Id is required")]
		public string ReceiverID { get; set; }
		
		[Required(ErrorMessage = "ActorID Id is required")]
		public string ActorID { get; set; }
		public NotificationType Content { get; set; }
	}
}