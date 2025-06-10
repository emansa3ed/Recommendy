using System;

namespace Entities.Exceptions
{
    public sealed class NotFoundOrganizationException : NotFoundException
    {
        public NotFoundOrganizationException(string id)
            : base($"No company or university found with id: {id}")
        {
        }
    }
} 