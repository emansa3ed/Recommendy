using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class OrganizationUnVerifiedBadRequestException : BadRequestException
	{
		public OrganizationUnVerifiedBadRequestException(string OrganizationId) : base($"Organization with {OrganizationId} is already unverified")
		{
		}
	}
}
