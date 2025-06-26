using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Service.Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;
using Shared.RequestFeatures;
using System.Collections.Concurrent;
using Shared.DTO.Scholaship;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Shared.DTO.Internship;
using Service.Ontology;

namespace Service
{
    internal  sealed class ScholarshipService: IScholarshipService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
		private readonly MyMemoryCache _memoryCache;
		private readonly ILogger<ScholarshipService> _logger;

        public ScholarshipService(IRepositoryManager repository, IMapper mapper, ILogger<ScholarshipService> logger,MyMemoryCache memoryCache)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_memoryCache = memoryCache;
		}


		public async Task<PagedList<EditedScholarshipDto>> GetAllScholarshipsForUniversity(string universityId, ScholarshipsParameters scholarshipsParameters, bool trackChanges)
        {

            var university = await _repository.university.GetUniversityAsync(universityId, trackChanges);

            if (university == null)
                throw new UniversityNotFoundException(universityId);

            var scholarships = await _repository.Scholarship.GetAllScholarshipsAsync(universityId, scholarshipsParameters, trackChanges);

			var res = _mapper.Map<List<EditedScholarshipDto>>(scholarships);

			return new PagedList<EditedScholarshipDto>(res, scholarships.MetaData.TotalCount, scholarshipsParameters.PageNumber, scholarshipsParameters.PageSize);
		}


		public async Task<EditedScholarshipDto> CreateScholarshipForUniversity(string universityId,
            ScholarshipForCreationDto scholarshipForCreation, bool trackChanges)
        {
            string url = _repository.File.UploadImage("Scholarships", scholarshipForCreation.ImageFile).Result;
           

            //    _logger.LogInformation("Mapping ScholarshipForCreationDto to Scholarship entity");
            var scholarshipEntity = _mapper.Map<Scholarship>(scholarshipForCreation);
            scholarshipEntity.UniversityId = universityId;
            scholarshipEntity.CreatedAt = DateTime.UtcNow;
            scholarshipEntity.UrlPicture = url;

            //    _logger.LogInformation("Creating scholarship in repository");
            _repository.Scholarship.CreateScholarship(scholarshipEntity);
            //    _logger.LogInformation("Saving changes to database");
            await _repository.SaveAsync();


            //   _logger.LogInformation("Mapping Scholarship entity to ScholarshipDto");
            var scholarshipToReturn = _mapper.Map<EditedScholarshipDto>(scholarshipEntity);

            return scholarshipToReturn;
        }

        public async Task<PagedList<GetScholarshipDto>> GetAllScholarships(ScholarshipsParameters scholarshipsParameters, bool trackChanges)
        {
			if (!_memoryCache.Cache.TryGetValue(scholarshipsParameters.ToString()+ "GetAllScholarships", out PagedList<Scholarship> cacheValue))
			{
				cacheValue = await _repository.Scholarship.GetAllScholarshipsAsync(scholarshipsParameters,trackChanges);

				var jsonSize = JsonSerializer.SerializeToUtf8Bytes(cacheValue).Length;


				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSize(jsonSize)
					.SetSlidingExpiration(TimeSpan.FromSeconds(5))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

				_memoryCache.Cache.Set(scholarshipsParameters.ToString() + "GetAllScholarships", cacheValue, cacheEntryOptions);
			}

			// Retrieve scholarships from the repository
			var scholarships = cacheValue;

            // Map scholarships to DTOs using AutoMapper
            var scholarshipDto = _mapper.Map<List<GetScholarshipDto>>(scholarships);

			return new PagedList<GetScholarshipDto>(scholarshipDto, scholarships.MetaData.TotalCount, scholarshipsParameters.PageNumber, scholarshipsParameters.PageSize); 

		}



		public async Task<GetScholarshipDto> GetScholarshipById(int id, bool trackChanges)
        {
            var scholarship = _repository.Scholarship.GetScholarshipById(id, trackChanges);
            if (scholarship == null)
                throw new ScholarshipNotFoundException(id);

            var scholarshipDto = _mapper.Map<GetScholarshipDto>(scholarship);
            return scholarshipDto;

        }

        public async Task<ScholarshipDto> GetScholarshipAsync(string universityId, int id, bool trackChanges)
        {
            var university = await _repository.university.GetUniversityAsync(universityId, trackChanges);
            if (university is null)
                throw new UniversityNotFoundException(universityId);

            var scholarshipDb = _repository.Scholarship.GetScholarship(universityId, id, trackChanges);
            if (scholarshipDb is null)
                throw new ScholarshipNotFoundException(id);

            var scholarship = _mapper.Map<ScholarshipDto>(scholarshipDb);
            return scholarship;
        }

        public async Task UpdateScholarshipForUniversityAsync(string universityId, int id, ScholarshipDto scholarshipDto, bool trackChanges)
        {
            var university = await _repository.university.GetUniversityAsync(universityId, trackChanges);
            if (university is null)
                throw new UniversityNotFoundException(universityId);

            var scholarship = _repository.Scholarship.GetScholarship(universityId, id, trackChanges);
            if (scholarship is null)
                throw new ScholarshipNotFoundException(id);

			_mapper.Map(scholarshipDto, scholarship);

			if (scholarshipDto.ImageFile != null)
            {
                var imageUrl = await _repository.File.UploadImage("Scholarship", scholarshipDto.ImageFile);
                scholarship.UrlPicture = imageUrl;
            }

            await _repository.SaveAsync();
        }


        public async Task DeleteScholarshipForUniversityAsync(string universityId, int id, bool trackChanges)
        {

            var university = await _repository.university.GetUniversityAsync(universityId, trackChanges);
            if (university is null)
                throw new UniversityNotFoundException(universityId);

            var scholarship = _repository.Scholarship.GetScholarship(universityId, id,trackChanges);
            if (scholarship is null)
                throw new ScholarshipNotFoundException(id);

            _repository.Scholarship.DeleteScholarship(scholarship);
			await _repository.SaveAsync();
		}

		public async Task<PagedList<GetScholarshipDto>> GetAllRecommendedScholarships(string UserSkills, string Titles, ScholarshipsParameters scholarshipsParameters, bool trackChanges)
		{

			if (!_memoryCache.Cache.TryGetValue(scholarshipsParameters.ToString() + UserSkills+ "GetAllRecommendedScholarships", out PagedList<Scholarship> cacheValue))
			{
				cacheValue = await _repository.Scholarship.GetAllRecommendedScholarships(Titles, scholarshipsParameters, trackChanges);

				var jsonSize = JsonSerializer.SerializeToUtf8Bytes(cacheValue).Length;

				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSize(jsonSize)
					.SetSlidingExpiration(TimeSpan.FromSeconds(240))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(320));
				_memoryCache.Cache.Set(scholarshipsParameters.ToString() + UserSkills + "GetAllRecommendedScholarships", cacheValue, cacheEntryOptions);
			}

			// Retrieve scholarships from the repository
			var scholarships = cacheValue;

			// Map scholarships to DTOs using AutoMapper
			var scholarshipDto = _mapper.Map<List<GetScholarshipDto>>(scholarships);

			return new PagedList<GetScholarshipDto>(scholarshipDto, scholarships.MetaData.TotalCount, scholarshipsParameters.PageNumber, scholarshipsParameters.PageSize);
		}

	}
}
