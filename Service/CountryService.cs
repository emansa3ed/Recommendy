using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Entities.Exceptions;
using Shared.DTO;
using AutoMapper;

namespace Service
{
    internal sealed class CountryService : ICountryService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CountryService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<CountryDto> GetAllCountries(bool trackChanges)
        {
            try
            {
                var countries =
             _repository.Country.GetAllCountries(trackChanges);

                var countriesDto = _mapper.Map<IEnumerable<CountryDto>>(countries);

                return countriesDto;
            }
            catch (Exception ex)
            {
                throw new Exception("something error");
            }
        }

        public CountryDto GetCountryById(int countryId, bool trackChanges)
        {
            var country = _repository.Country.GetCountryById(countryId, trackChanges);
            if (country is null)
                throw new CountryNotFoundException(countryId);

            return _mapper.Map<CountryDto>(country);
        }
    }
}
