using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ICountryRepository 
    {
        IEnumerable<Country> GetAllCountries(bool trackChanges);
        Country GetCountryById(int countryId, bool trackChanges);
    }
}
