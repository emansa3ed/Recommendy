using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ICountryRepository> _countryRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _countryRepository = new Lazy<ICountryRepository>(() => new
          CountryRepository(repositoryContext));
        }

        public ICountryRepository Country => _countryRepository.Value;

        public void Save() => _repositoryContext.SaveChanges();

    }
}
