using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class UniversityNotFoundException : NotFoundException
    {
        public UniversityNotFoundException(string universityId)
        : base($"The university with id: {universityId} doesn't exist in the database.") 
        {
        }
    }
}
