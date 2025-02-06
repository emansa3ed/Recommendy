using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class CountryNotFoundException : NotFoundException
    {
        public CountryNotFoundException(int countryId)
        : base($"The country with id: {countryId} doesn't exist in the database.") 
        {
        }
    }
}
