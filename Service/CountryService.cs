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
using AutoMapper;
using Shared.DTO.Country;
using Microsoft.Extensions.Caching.Memory;
using Shared.RequestFeatures;
using System.Text.Json;

namespace Service
{
    internal sealed class CountryService : ICountryService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly MyMemoryCache _memoryCache;


		public CountryService(IRepositoryManager repository, IMapper mapper, MyMemoryCache memoryCache)
        {
            _repository = repository;
            _mapper = mapper;
            _memoryCache = memoryCache;
		}

        public async Task<IEnumerable<CountryDto>> GetAllCountriesAsync(bool trackChanges)
        {


			if (!_memoryCache.Cache.TryGetValue("Countries" , out IEnumerable<Country> cacheValue))
			{
				cacheValue = await _repository.Country.GetAllCountriesAsync(trackChanges);

				var jsonSize = JsonSerializer.SerializeToUtf8Bytes(cacheValue).Length;


				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSize(jsonSize)
					.SetSlidingExpiration(TimeSpan.FromSeconds(5))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

				_memoryCache.Cache.Set("Countries", cacheValue, cacheEntryOptions);
			}

            var countries = cacheValue;

            var countriesDto = _mapper.Map<IEnumerable<CountryDto>>(countries);

            return countriesDto;
           
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
