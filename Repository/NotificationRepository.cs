using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;


namespace Repository
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
	{
		public NotificationRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
		{


		}

		public void CreateNotification(Notification notification) => Create(notification);

		public async Task<PagedList<Notification>> GetAllNotificationskAsync(string ReceiverID, NotificationParameters notification, bool TrackChanges = false)
		{
			var res = await FindByCondition(n => n.ReceiverID == ReceiverID, TrackChanges)
			.Paging(notification.PageNumber, notification.PageSize)
			.ToListAsync();

			var count = await FindByCondition(n => n.ReceiverID == ReceiverID, TrackChanges).CountAsync();

			return new PagedList<Notification>(res, count, notification.PageNumber, notification.PageSize);
		}

		public async Task<Notification> GetNotificationAsync(string ReceiverID, int NotificationId,bool TrackChanges =false)
		{
			return await FindByCondition(n => (n.Id == NotificationId && n.ReceiverID == ReceiverID), TrackChanges).SingleOrDefaultAsync();
		}
	}
}
