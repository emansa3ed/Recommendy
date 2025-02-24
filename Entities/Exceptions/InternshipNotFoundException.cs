using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class InternshipNotFoundException : NotFoundException
	{
		public InternshipNotFoundException(int id)
		   : base($"Internship with id: {id} doesn't exist in the database.")
		{

		}
	}
}
