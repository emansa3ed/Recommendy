using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
	public static  class RepositoryNotificationExtensions
	{
		public static IQueryable<Notification> Paging(this IQueryable<Notification> Notification, int PageNumber, int PageSize)
		{
			return Notification
			.Skip((PageNumber - 1) * PageSize)
			.Take(PageSize);
		}
	}
}
