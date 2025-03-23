using Microsoft.AspNetCore.SignalR;
using Service.Contracts;
using Shared.DTO.Feedback;
using Shared.DTO.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Hubs
{
	public class FeedbackHub : Hub
	{
		private readonly IFeedbackService _feedbackService;
		public FeedbackHub(IFeedbackService feedbackService)
		{
			_feedbackService = feedbackService;
		}

		public async Task SendNotification(string CompanyID, int PostId,FeedbackCreationDto feedback)
		{
			await _feedbackService.CreateFeedbackAsync(CompanyID,PostId, feedback.StudentId, feedback);
			string jsonData = JsonSerializer.Serialize(new {CompanyID,PostId, feedback.StudentId, feedback});
			await Clients.All.SendAsync("ReceiveFeedback", jsonData);
		}

	}
}
