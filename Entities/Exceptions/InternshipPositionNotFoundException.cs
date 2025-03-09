using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class InternshipPositionNotFoundException :NotFoundException
    {
        public InternshipPositionNotFoundException()
        : base($"The InternshipPosition With this Ids :  doesn't exist in the database.")
        {
        }
    }
}
