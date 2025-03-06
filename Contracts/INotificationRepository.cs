using Entities.Models;
using Shared.DTO.Feedback;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface INotificationRepository
    {
		Task<PagedList<Notification>> GetAllNotificationskAsync(string ReceiverID, NotificationParameters notification, bool TrackChanges = false);
		Task<Notification> GetNotificationAsync(string ReceiverID, int NotificationId, bool TrackChanges = false);

		void CreateNotification(Notification notification);

	}
}
