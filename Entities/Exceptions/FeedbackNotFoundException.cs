using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public  class FeedbackNotFoundException : NotFoundException
	{
		public FeedbackNotFoundException(int FeedBackId)
		: base($"The FeedBack with id: {FeedBackId} doesn't exist in the database.")
		{
		}
	}
}
