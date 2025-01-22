using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICountryService> _countryService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _countryService = new Lazy<ICountryService>(() => new
          CountryService(repositoryManager));
           
        }

        public ICountryService CountryService => _countryService.Value;
}
}
