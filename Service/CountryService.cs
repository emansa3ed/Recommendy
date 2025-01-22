using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;

namespace Service
{
    internal sealed class CountryService : ICountryService
    {
        private readonly IRepositoryManager _repository;

        public CountryService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public IEnumerable<Country> GetAllCountries(bool trackChanges)
 {
                var companies =
             _repository.Country.GetAllCountries(trackChanges);

                return companies;
           
        }
    }
}
