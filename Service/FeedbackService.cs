using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO;
using Shared.DTO.Feedback;
using Shared.DTO.Notification;
using Shared.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.SignalR;
using Service.Hubs;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    internal sealed class FeedbackService : IFeedbackService
    {

		private readonly IRepositoryManager _repository;
		private readonly IMapper _mapper;
		private readonly INotificationService _notificationService;
		private readonly IHubContext<FeedbackHub> _hubContext;

		public FeedbackService(IRepositoryManager repository, IMapper mapper, INotificationService notificationService, IHubContext<FeedbackHub> hubContext)
		{
			_hubContext = hubContext;
			_repository = repository;
			_mapper = mapper;
			_notificationService = notificationService;
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
				post = _repository.Intership.GetInternshipById(CompanyID, PostId, false);
				if (post == null)
					throw new   InternshipNotFoundException(PostId);
			}

			var res = await _repository.FeedbackRepository.GetFeedbackByUserId(PostId, StudentId, feedback.Type, false);
			if (res != null)
				throw new BadRequestException("You have already submitted feedback for this post.");

			var feedbackEntity = _mapper.Map<Feedback>(feedback);

			feedbackEntity.StudentId = StudentId;
			feedbackEntity.PostId = PostId;
			feedbackEntity.CreatedAt = DateTime.UtcNow;

			_repository.FeedbackRepository.CreateFeedback(feedbackEntity);

			await _repository.SaveAsync();

			await _notificationService.CreateNotificationAsync(new NotificationCreationDto { ActorID = StudentId,
				ReceiverID = CompanyID,
				Content = NotificationType.CreateFeedBack,
			PostID=PostId});
			string jsonData = JsonSerializer.Serialize(feedbackEntity);
			await _hubContext.Clients.All.SendAsync("ReceiveNotification", jsonData);
		}

		public async  Task DeleteFeedbackAsync(string CompanyID, string StudentId, int PostId, FeedbackDelationDto FeedbackId)
		{
			object post;
			if (FeedbackId.Type == FeedbackType.Scholarship)
			{
				post = _repository.Scholarship.GetScholarship(CompanyID, PostId, false);
				if (post == null)
					throw new ScholarshipNotFoundException(PostId);
			}
			else
			{
				post = _repository.Intership.GetInternshipById(CompanyID, PostId, false);
				if (post == null)
					throw new InternshipNotFoundException(PostId);
			}

			var feedback = await _repository.FeedbackRepository.GetFeedbackById(FeedbackId.Id, false);

			if (feedback == null)
				throw new FeedbackNotFoundException(FeedbackId.Id);

			if (feedback.PostId != PostId || feedback.StudentId!= StudentId)
				throw new BadRequestException("Invalid data");
			feedback.Student = null;
			_repository.FeedbackRepository.DeleteFeedback(feedback);

			await _repository.SaveAsync();
			string jsonData = JsonSerializer.Serialize(feedback);
			await _hubContext.Clients.All.SendAsync("FeedbackDeleted", jsonData);
		}

		public async Task<FeedBackDto> GetFeedbackAsync(int FeedbackId)
		{

			var feedback = await _repository.FeedbackRepository.GetFeedbackById(FeedbackId, false);


			if (feedback == null)
				throw new FeedbackNotFoundException(FeedbackId);

			var res =_mapper.Map<FeedBackDto>(feedback);
			return res;
		}


		public async Task<PagedList<FeedBackDto>> GetAllFeedbackAsync(string CompanyID, int PostId, FeedBackParameters Feedback)
		{

			object post;
			if (Feedback.type == FeedbackType.Scholarship)
			{
				post = _repository.Scholarship.GetScholarship(CompanyID, PostId, false);
				if (post == null)
					throw new ScholarshipNotFoundException(PostId);
			}
			else
			{
				post = _repository.Intership.GetInternshipById(CompanyID,PostId, false);
				if (post == null)
					throw new InternshipNotFoundException(PostId);
			}

			var feedbacks = await _repository.FeedbackRepository.GetAllFeedbackAsync(PostId,Feedback, false);

			var res = _mapper.Map<List<FeedBackDto>>(feedbacks);

			return new PagedList<FeedBackDto>(res, feedbacks.MetaData.TotalCount, Feedback.PageNumber, Feedback.PageSize);
		}

		public async Task EditFeedBack(string CompanyID, string StudentId, int PostId, FeedbackEditDto feedbackEditDto)
		{
			await _repository.FeedbackRepository.EditFeedBack(CompanyID, StudentId, PostId, feedbackEditDto);
			await _repository.SaveAsync();
		}
	}
}

