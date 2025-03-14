using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {

        public CountryRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

            
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync(bool trackChanges) =>
           await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        public Country GetCountryById(int countryId, bool trackChanges) =>
        FindByCondition(c => c.Id == countryId, trackChanges)
        .SingleOrDefault();

		public int CreateCountry(Country Country)
        {
            Create(Country);
            return Country.Id;
		}
	}
}
