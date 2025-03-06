using Shared.DTO.Feedback;
using Shared.DTO.Notification;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface INotificationService
    {
		Task CreateNotificationAsync(NotificationCreationDto notification);
		Task<NotificationDto> GetNotificationAsync(string ReceiverID,int NotificationId, bool TrackChanges = false);
		Task<PagedList<NotificationDto>> GetAllNotificationskAsync(string ReceiverID, NotificationParameters notification, bool TrackChanges = false);
	}
}
