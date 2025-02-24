using Entities.Models;
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

		Task<Feedback> GetFeedbackById(int FeedbackId, bool TrackChanges = false);
	}
}
