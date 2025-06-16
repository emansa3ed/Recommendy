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
using Shared.DTO.Notification;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace Service
{
    internal sealed class UniversityService: IUniversityService
    {
        private readonly IRepositoryManager _repository;
        private readonly IServiceManager _service;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
		private readonly INotificationService _notificationService;

		public UniversityService(IRepositoryManager repository, IServiceManager service,  IMapper mapper, INotificationService notificationService, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _service = service;
            _notificationService = notificationService;
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

        public async Task VerifyUniversity(string universityId, string adminId, UniversityVerificationDto verificationDto, bool trackChanges)
        {
            var university = await _repository.university.GetUniversityAsync(universityId, trackChanges);
            if (university == null)
                throw new UniversityNotFoundException(universityId);
            if (university.IsVerified && verificationDto.IsVerified)
                throw new OrganizationVerifiedBadRequestException(university.UniversityId);
			if (!university.IsVerified && !verificationDto.IsVerified)
				throw new OrganizationUnVerifiedBadRequestException(university.UniversityId);

			university.IsVerified = verificationDto.IsVerified;
            university.VerificationNotes = verificationDto.VerificationNotes;

            _repository.university.UpdateUniversity(university);
            await _repository.SaveAsync();

            if (verificationDto.IsVerified)
			    await _notificationService.CreateNotificationAsync(new NotificationCreationDto
			    {
				    ActorID = adminId,
				    ReceiverID = universityId,
				    Content = NotificationType.OrganizationVerified
			    });

			var (subject, message) = EmailTemplates.Organization.GetVerificationTemplate(
                verificationDto.IsVerified,
                verificationDto.VerificationNotes);
            await _service.EmailsService.Sendemail(university.User.Email, message, subject);
        }



    }
}
