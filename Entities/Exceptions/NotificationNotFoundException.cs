using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class NotificationNotFoundException : NotFoundException
	{
		public NotificationNotFoundException(string ReceiverID,int NotificationId)
		: base($"The Notification with id: {NotificationId} for user with id {ReceiverID} doesn't exist in the database.")
		{
		}
	}
}
