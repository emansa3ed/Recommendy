using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IFeedbackRepository
	{
		void CreateFeedback(Feedback feedback);
		void DeleteFeedback(Feedback feedback);

		Task<Feedback> GetFeedbackById(int? FeedbackId, bool TrackChanges = false);
		Task<Feedback> GetFeedbackByUserId(int PostId, string UserId,FeedbackType feedbackType, bool TrackChanges = false);
		Task<PagedList<Feedback>> GetAllFeedbackAsync(int PostId, FeedBackParameters feedBack, bool TrackChanges = false);
	}
}
