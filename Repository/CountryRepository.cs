using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;

namespace Repository
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {

        public CountryRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

            
        }

        public IEnumerable<Country> GetAllCountries(bool trackChanges) =>
           FindAll(trackChanges).OrderBy(c => c.Name).ToList();
        public Country GetCountryById(int countryId, bool trackChanges) =>
        FindByCondition(c => c.Id == countryId, trackChanges)
        .SingleOrDefault();

    }
}
