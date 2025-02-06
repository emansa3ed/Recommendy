using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class StudentNotFoundException : NotFoundException
    {
        public StudentNotFoundException(string studentId)
        : base($"The student with id: {studentId} doesn't exist in the database.")
        {
        }
    }
}
