using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ReportNotFoundException : NotFoundException
    {
        public ReportNotFoundException(int ReportId)
       : base($"The Report with id: {ReportId} doesn't exist in the database.")
        {
        }
    }
}
