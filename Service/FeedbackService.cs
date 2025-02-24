using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO.Feedback;

namespace Service
{
	internal sealed class FeedbackService :IFeedbackService
    {
		private readonly IRepositoryManager _repository;
		private readonly IMapper _mapper;
		public FeedbackService(IRepositoryManager repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateFeedbackAsync(string CompanyID, int PostId,string StudentId, FeedbackCreationDto feedback)
		{
			var student = _repository.Student.GetStudent(StudentId, false);
			if (student == null)
				throw new StudentNotFoundException(StudentId);

			object post;
			if (feedback.Type == FeedbackType.Scholarship)
			{
				post = _repository.Scholarship.GetScholarship(CompanyID,PostId, false);
				if (post == null)
					throw new ScholarshipNotFoundException(PostId);
			}
			else
			{
				post = _repository.Intership.GetInternshipById(PostId, false);
				if (post == null)
					throw new   InternshipNotFoundException(PostId);
			}

			var feedbackEntity = _mapper.Map<Feedback>(feedback);

			feedbackEntity.StudentId = StudentId;
			feedbackEntity.PostId = PostId;
			feedbackEntity.CreatedAt = DateTime.UtcNow;

			_repository.FeedbackRepository.CreateFeedback(feedbackEntity);

			await _repository.SaveAsync();

		}

		public async  Task DeleteFeedbackAsync(string CompanyID, int PostId, string StudentId, FeedbackDelationDto FeedbackId)
		{
			var student = _repository.Student.GetStudent(StudentId, false);
			if (student == null)
				throw new StudentNotFoundException(StudentId);

			object post;
			if (FeedbackId.Type == FeedbackType.Scholarship)
			{
				post = _repository.Scholarship.GetScholarship(CompanyID, PostId, false);
				if (post == null)
					throw new ScholarshipNotFoundException(PostId);
			}
			else
			{
				post = _repository.Intership.GetInternshipById(PostId, false);
				if (post == null)
					throw new InternshipNotFoundException(PostId);
			}

			var feedback = await _repository.FeedbackRepository.GetFeedbackById(FeedbackId.Id, false);

			if (feedback == null)
				throw new FeedbackNotFoundException(FeedbackId.Id);

			_repository.FeedbackRepository.DeleteFeedback(feedback);

			await _repository.SaveAsync();
		}
	}
}
