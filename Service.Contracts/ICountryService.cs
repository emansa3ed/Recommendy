using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;
using Entities.Models;
namespace Service.Contracts
{
    public interface ICountryService
    {

        IEnumerable<CountryDto> GetAllCountries(bool trackChanges);
        CountryDto GetCountryById(int countryId, bool trackChanges);

    }
}
