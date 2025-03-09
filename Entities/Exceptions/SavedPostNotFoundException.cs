using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class SavedPostNotFoundException :NotFoundException
    {
        public SavedPostNotFoundException()
        : base($"There is no Saved post for this Ids : n't exist in the database.")
        {
        }
    }
}
