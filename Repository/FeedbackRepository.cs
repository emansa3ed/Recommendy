using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.DTO.Feedback;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {



		}
		public void CreateFeedback(Feedback feedback) => Create(feedback);
		public void DeleteFeedback(Feedback feedback) => Delete(feedback);
		public async Task<Feedback> GetFeedbackById(int? FeedbackId,bool TrackChanges =false) => await FindByCondition(f => f.Id == FeedbackId, TrackChanges)
			.Include(f=>f.Student)
			.Include(f => f.Student)
			.ThenInclude(s => s.User)
			.SingleOrDefaultAsync();
		public async Task<PagedList<Feedback>> GetAllFeedbackAsync(int PostId,FeedBackParameters feedBack, bool TrackChanges =false)
		{
			var res = await FindByCondition(f=>f.PostId == PostId && f.Type.Equals(feedBack.type) , TrackChanges)
				.Include(f=>f.Student)
				.ThenInclude(s => s.User)
				.AsSplitQuery()
				.Paging(feedBack.PageNumber,feedBack.PageSize)
				.ToListAsync();


            var count = await FindByCondition(f => f.PostId == PostId && f.Type.Equals(feedBack.type), TrackChanges).CountAsync();

            return new PagedList<Feedback>(res, count, feedBack.PageNumber, feedBack.PageSize);

        }

		public async Task<Feedback> GetFeedbackByUserId(int PostId,string UserId, FeedbackType feedbackType, bool TrackChanges = false) =>
			await FindByCondition(f => f.PostId == PostId && f.Type.Equals(feedbackType) && f.StudentId.Equals(UserId), TrackChanges)
			.FirstOrDefaultAsync();

		public async Task EditFeedBack(string CompanyID, string StudentId, int PostId, FeedbackEditDto feedbackEditDto)
		{
			var feedback = await FindByCondition(f => f.PostId == PostId && f.StudentId.Equals(StudentId) && f.Type.Equals(feedbackEditDto.Type), true)
				.FirstOrDefaultAsync();

			if (feedback == null)
				throw new FeedbackNotFoundException(null);

			feedback.Content = feedbackEditDto.Content;
			Update(feedback);
		}
	}
}