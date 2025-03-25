using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ChatNotFoundException : NotFoundException
    {
      public  ChatNotFoundException(int ChatId)
        : base($"The Chat with id: {ChatId} doesn't exist in the database.")
        {
        }
    }
}
