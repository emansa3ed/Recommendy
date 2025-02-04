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


        public async Task<IEnumerable<GetScholarshipDto>> GetAllScholarshipsForUniversity(string universityId, bool trackChanges)
        {
            try
            {
                if (string.IsNullOrEmpty(universityId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }
                // Retrieve scholarships for the logged-in university
                var scholarships = await _repository.Scholarship.GetAllScholarshipsAsync(universityId, trackChanges);
                return _mapper.Map<IEnumerable<GetScholarshipDto>>(scholarships);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving scholarships.", ex);
            }

        }


        public async Task<GetScholarshipDto> CreateScholarshipForUniversity(string universityId,
            ScholarshipForCreationDto scholarshipForCreation, bool trackChanges)
        {
            string url = _repository.File.UploadImage("Scholarships", scholarshipForCreation.ImageFile).Result;
           

            try
            {
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
                var scholarshipToReturn = _mapper.Map<GetScholarshipDto>(scholarshipEntity);

                return scholarshipToReturn;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "Error mapping scholarship data");
                throw new Exception("Error occurred while mapping scholarship data", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating scholarship for university {UniversityId}", universityId);
                throw new Exception($"Error creating scholarship: {ex.Message}", ex);
            }
        }


        public async Task<IEnumerable<GetScholarshipDto>> GetAllScholarships(bool trackChanges)
        {
            try
            {
                var scholarships = _repository.Scholarship.GetAllScholarships(trackChanges);
                var scholarshipDto = _mapper.Map<IEnumerable<GetScholarshipDto>>(scholarships);
                return await Task.FromResult(scholarshipDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching scholarships.");
                throw new Exception("Something went wrong while fetching scholarships.");
            }
        }


        public async Task<GetScholarshipDto> GetScholarshipById(int id, bool trackChanges)
        {
            try
            {
                var scholarship = _repository.Scholarship.GetScholarshipById(id, trackChanges);
                var scholarshipDto = _mapper.Map<GetScholarshipDto>(scholarship);
                return scholarshipDto;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving scholarship", ex);
            }
        }








        public ScholarshipDto GetScholarship(string universityId, int id, bool trackChanges)
        {
            var university = _repository.university.GetUniversity(universityId, trackChanges);
            if (university is null)
                throw new UniversityNotFoundException(universityId);

            var scholarshipDb = _repository.Scholarship.GetScholarship(universityId, id, trackChanges);
            if (scholarshipDb is null)
                throw new ScholarshipNotFoundException(id);

            var scholarship = _mapper.Map<ScholarshipDto>(scholarshipDb);
            return scholarship;
        }

        public async Task UpdateScholarshipForUniversity(string universityId, int id, ScholarshipDto scholarshipDto, bool trackChanges)
        {
            var university = _repository.university.GetUniversity(universityId, trackChanges);
            if (university is null)
                throw new UniversityNotFoundException(universityId);

            var scholarship = _repository.Scholarship.GetScholarship(universityId, id, trackChanges);
            if (scholarship is null)
                throw new ScholarshipNotFoundException(id);

            
            if (scholarshipDto.Name != null)
                scholarship.Name = scholarshipDto.Name;

            if (scholarshipDto.Description != null)
                scholarship.Description = scholarshipDto.Description;

            if (scholarshipDto.Cost.HasValue)
                scholarship.Cost = scholarshipDto.Cost.Value;

            if (scholarshipDto.Grants != null)
                scholarship.Grants = scholarshipDto.Grants;

            if (scholarshipDto.ApplicationDeadline.HasValue)
                scholarship.ApplicationDeadline = scholarshipDto.ApplicationDeadline.Value;

            if (scholarshipDto.UrlApplicationForm != null)
                scholarship.UrlApplicationForm = scholarshipDto.UrlApplicationForm;

            if (scholarshipDto.UrlPicture != null)
                scholarship.UrlPicture = scholarshipDto.UrlPicture;

            if (scholarshipDto.EligibleGrade != null)
                scholarship.EligibleGrade = scholarshipDto.EligibleGrade;

            if (scholarshipDto.StartDate.HasValue)
                scholarship.StartDate = scholarshipDto.StartDate.Value;

            if (scholarshipDto.Duration.HasValue)
                scholarship.Duration = scholarshipDto.Duration.Value;

            if (scholarshipDto.Degree.HasValue)
                scholarship.Degree = scholarshipDto.Degree.Value;

            if (scholarshipDto.Funded.HasValue)
                scholarship.Funded = scholarshipDto.Funded.Value;

            if (scholarshipDto.Requirements != null)
                scholarship.Requirements = scholarshipDto.Requirements;
            if (scholarshipDto.ImageFile != null)
            {
                try
                {
                    var imageUrl = await _repository.File.UploadImage("Scholarship", scholarshipDto.ImageFile);
                    scholarship.UrlPicture = imageUrl;
                }
                catch (ArgumentException ex)
                {
                    throw new BadRequestException(ex.Message);
                }
            }

            _repository.Save();
        }


        public void DeleteScholarshipForUniversity(string universityId, int id, bool trackChanges)
        {

            var university = _repository.university.GetUniversity(universityId, trackChanges);
            if (university is null)
                throw new UniversityNotFoundException(universityId);

            var scholarship = _repository.Scholarship.GetScholarship(universityId, id,trackChanges);
            if (scholarship is null)
                throw new ScholarshipNotFoundException(id);

            _repository.Scholarship.DeleteScholarship(scholarship);
            _repository.Save();
        }
    }
}
