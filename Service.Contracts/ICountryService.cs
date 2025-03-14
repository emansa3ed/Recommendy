using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.DTO.Country;
namespace Service.Contracts
{
    public interface ICountryService
    {

        Task<IEnumerable<CountryDto>> GetAllCountriesAsync(bool trackChanges);
        CountryDto GetCountryById(int countryId, bool trackChanges);

    }
}
