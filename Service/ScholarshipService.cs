using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DTO;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;

namespace Service
{
    internal  sealed class ScholarshipService: IScholarshipService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ScholarshipService> _logger;

        public ScholarshipService(IRepositoryManager repository, IMapper mapper, ILogger<ScholarshipService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<IEnumerable<EditedScholarshipDto>> GetAllScholarshipsForUniversity(string universityId, bool trackChanges)
        {

            var university = await _repository.university.GetUniversityAsync(universityId, trackChanges);

            if (university == null)
                throw new UniversityNotFoundException(universityId);

            var scholarships = await _repository.Scholarship.GetAllScholarshipsAsync(universityId, trackChanges);
            return _mapper.Map<IEnumerable<EditedScholarshipDto>>(scholarships);

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

        public async Task<IEnumerable<GetScholarshipDto>> GetAllScholarships(bool trackChanges)
        {
            try
            {
                // Retrieve scholarships from the repository
                var scholarships =  _repository.Scholarship.GetAllScholarships(trackChanges);

                // Map scholarships to DTOs using AutoMapper
                var scholarshipDto = _mapper.Map<IEnumerable<GetScholarshipDto>>(scholarships);

                return scholarshipDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching scholarships.");
                throw new Exception("Something went wrong while fetching scholarships.");
            }
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
	}
}
