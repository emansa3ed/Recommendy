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


		public async Task SendFeedBack(string CompanyID, int PostId,string StudentId,FeedbackCreationDto feedback)
		{
			await _feedbackService.CreateFeedbackAsync(CompanyID,PostId, StudentId, feedback);
			string jsonData = JsonSerializer.Serialize(new {CompanyID,PostId, StudentId, feedback});
			await Clients.All.SendAsync("ReceiveFeedback", jsonData);
		}

		public async Task DeleteFeedback(string companyId, int postId, string studentId)
		{
			await _feedbackService.DeleteFeedbackAsync(companyId, postId, studentId);

			string jsonData = JsonSerializer.Serialize(new { companyId, postId, studentId });
			await Clients.All.SendAsync("FeedbackDeleted", jsonData);
		}

	}
}