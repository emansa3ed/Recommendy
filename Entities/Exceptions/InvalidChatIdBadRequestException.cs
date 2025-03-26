using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
	public class InvalidChatIdBadRequestException :BadRequestException
	{
		public InvalidChatIdBadRequestException(int ChatId) : base($"{ChatId} is invalid ChatId for given user id")
		{
		}
	}
}
