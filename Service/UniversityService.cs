using AutoMapper;
using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Shared.DTO;

namespace Service
{
    internal sealed class UniversityService: IUniversityService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UniversityService(IRepositoryManager repository, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public UniversityViewDto GetUniversity(string id, bool trackChanges)
        {
            var university = _repository.university.GetUniversity(id, trackChanges);
            if (university is null)
                throw new UniversityNotFoundException(id);
            var universityviewDto = _mapper.Map<UniversityViewDto>(university);
            return universityviewDto;
        }

        public async Task UpdateUniversity(string universityId, UniversityDto universityDto, bool trackChanges)
        {
            var university = _repository.university.GetUniversity(universityId, trackChanges);
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
            
           
        
        _repository.university.UpdateUniversity(university);
        _repository.Save();
        }

        

    }
}
