using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO.Feedback;
using Shared.DTO.Notification;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public class NotificationService : INotificationService
	{
		private readonly IRepositoryManager _repository;
		private readonly IMapper _mapper;

		public NotificationService(IRepositoryManager repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task CreateNotificationAsync(NotificationCreationDto notification)
		{
			var receiver = await _repository.User.GetById(notification.ReceiverID);
			if (receiver == null)
				throw new UserNotFoundException(notification.ReceiverID);
			var actor = await _repository.User.GetById(notification.ActorID);
			if (actor == null)
				throw new UserNotFoundException(notification.ActorID);
			var notificationEntity = _mapper.Map<Notification>(notification);
			notificationEntity.CreatedAt = DateTime.UtcNow;
			notificationEntity.IsRead = false;
			_repository.NotificationRepository.CreateNotification(notificationEntity);
			await _repository.SaveAsync();
		}

		public async Task<PagedList<NotificationDto>> GetAllNotificationskAsync(string ReceiverID, NotificationParameters notification, bool TrackChanges = false)
		{
			var receiver = await _repository.User.GetById(ReceiverID);
			if (receiver == null)
				throw new UserNotFoundException(ReceiverID);
			var notifications = await _repository.NotificationRepository.GetAllNotificationskAsync(ReceiverID, notification, TrackChanges);

			var res = _mapper.Map<List<NotificationDto>>(notifications);

			return new PagedList<NotificationDto>(res, notifications.MetaData.TotalCount, notification.PageNumber, notification.PageSize);
		}

		public async Task<NotificationDto> GetNotificationAsync(string ReceiverID, int NotificationId, bool TrackChanges = false)
		{
			var receiver = await _repository.User.GetById(ReceiverID);
			if (receiver == null)
				throw new UserNotFoundException(ReceiverID);
			var notification = await _repository.NotificationRepository.GetNotificationAsync(ReceiverID, NotificationId, TrackChanges);
			if (notification == null)
				throw new NotificationNotFoundException(ReceiverID,NotificationId);

			var res = _mapper.Map<NotificationDto>(notification);

			return res;
		}
	}
}
