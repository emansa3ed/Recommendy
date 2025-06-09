using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    
    
        public sealed class UserDeletionFailedException : BadRequestException
        {
            public UserDeletionFailedException(string userId)
                : base($"Failed to delete user with id: {userId}.")
            { }

            public UserDeletionFailedException(string userId, string reason)
                : base($"Failed to delete user with id: {userId}. Reason: {reason}")
            { }
        }
    
}
