using AutoMapper;
using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Shared.DTO.University;
using Shared.DTO.Company;
using Stripe;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class UniversityService: IUniversityService
    {
        private readonly IRepositoryManager _repository;
        private readonly IServiceManager _service;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UniversityService(IRepositoryManager repository, IServiceManager service,  IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _service = service;
        }
        public async Task<UniversityViewDto> GetUniversityAsync(string id, bool trackChanges)
        {
            var university = await _repository.university.GetUniversityAsync(id, trackChanges);
            if (university is null)
                throw new UniversityNotFoundException(id);
            var universityviewDto = _mapper.Map<UniversityViewDto>(university);
            return universityviewDto;
        }

        public async Task UpdateUniversity(string universityId, UniversityDto universityDto, bool trackChanges)
        {
            var university = await _repository.university.GetUniversityAsync(universityId, trackChanges);
            if (university == null)
            {
                throw new UniversityNotFoundException(universityId);
            }
            if (!string.IsNullOrEmpty(universityDto.UniversityUrl))
            {
                university.UniversityUrl = universityDto.UniversityUrl;
            }
            if (universityDto.CountryId.HasValue)
            {
                university.CountryId = universityDto.CountryId;
            }
                     
            if (!string.IsNullOrEmpty(universityDto.Bio))
            {
                university.User.Bio = universityDto.Bio;
            }
            if (!string.IsNullOrEmpty(universityDto.PhoneNumber))
            {
                university.User.PhoneNumber = universityDto.PhoneNumber;
            }
        
            await _repository.SaveAsync();
        }

        public async Task<PagedList<UniversityViewDto>> GetUnverifiedUniversitiesAsync(
    UniversityParameters universityParameters, bool trackChanges)
        {
            var universities = await _repository.university.GetUnverifiedUniversitiesAsync(universityParameters, trackChanges);

            var universityDtos = _mapper.Map<List<UniversityViewDto>>(universities);

            return new PagedList<UniversityViewDto>(
                universityDtos,
                universities.MetaData.TotalCount,
                universities.MetaData.CurrentPage,
                universities.MetaData.PageSize);
        }

        public async Task VerifyUniversity(string universityId, UniversityVerificationDto verificationDto, bool trackChanges)
        {
            var university = await _repository.university.GetUniversityAsync(universityId, trackChanges);
            if (university == null)
                throw new UniversityNotFoundException(universityId);

            university.IsVerified = verificationDto.IsVerified;
            university.VerificationNotes = verificationDto.VerificationNotes;

            _repository.university.UpdateUniversity(university);
            await _repository.SaveAsync();

            await _service.EmailsService.SendVerificationEmail(
                university.User.Email,
                university.IsVerified,
                university.VerificationNotes);
        }



    }
}
