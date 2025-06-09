using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.RequestFeatures;
using Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Service
{
    internal sealed class AdminService : IAdminService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IEmailsService _emailsService;
        private readonly UserManager<User> _userManager;
        public AdminService(IRepositoryManager repository, IMapper mapper, IEmailsService emailsService, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _emailsService = emailsService;
            _userManager = userManager;
        }

        public async Task<PagedList<UserDto>> GetUsersAsync(UsersParameters parameters, bool trackChanges)
        {
            var users = await _repository.Admin.GetUsersAsync(parameters, trackChanges);

            var userDtos = _mapper.Map<List<UserDto>>(users);

            return new PagedList<UserDto>(
                userDtos,
                users.MetaData.TotalCount,
                users.MetaData.CurrentPage,
                users.MetaData.PageSize);
        }

        public async Task<UserDto> GetUserByIdAsync(string userId, bool trackChanges)
        {
            var user = await _repository.Admin.GetUserByIdAsync(userId, trackChanges);
            if (user is null)
                throw new UserNotFoundException(userId);

            return _mapper.Map<UserDto>(user);
        }

        public async Task BanUserAsync(string userId, UserBanDto banDto, bool trackChanges)
        {
            var user = await _repository.Admin.GetUserByIdAsync(userId, trackChanges);
            if (user is null)
                throw new UserNotFoundException(userId);

            user.IsBanned = true;
            await _repository.SaveAsync();

            // Send ban notification using the Reason property
            await _emailsService.SendBanEmail(user.Email, banDto.Reason);
        }

        public async Task UnbanUserAsync(string userId, bool trackChanges)
        {
            var user = await _repository.Admin.GetUserByIdAsync(userId, trackChanges);
            if (user is null)
                throw new UserNotFoundException(userId);

            user.IsBanned = false;
            await _repository.SaveAsync();

            // Send unban notification with simplified method
            await _emailsService.SendUnbanEmail(user.Email);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _repository.Admin.GetUserByIdAsync(userId, true);
            if (user is null)
                throw new UserNotFoundException(userId);

            try
            {
                switch (user.Discriminator)
                {
                    case "University":
                        await DeleteUniversityData(user.University);
                        break;
                    case "Company":
                        await DeleteCompanyData(user.Company);
                        break;
                    case "Student":
                        await DeleteStudentData(user.Student);
                        break;
                    default:
                        throw new UserDeletionFailedException(userId, $"Unknown user type: {user.Discriminator}");
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new UserDeletionFailedException(userId, errors);
                }

                await _repository.SaveAsync();
            }
            catch (Exception ex) when (ex is not UserNotFoundException && ex is not UserDeletionFailedException)
            {
                
                throw new UserDeletionFailedException(userId, ex.Message);
            }
        }

        //helper methods to simplify deletion logic
        private async Task DeleteUniversityData(University university)
        {
            if (university != null)
            {
                await _repository.Scholarship.DeleteScholarshipsByUniversityId(university.UniversityId);
                _repository.university.DeleteUniversity(university);
            }
        }

        private async Task DeleteCompanyData(Company company)
        {
            if (company != null)
            {
                await _repository.Intership.DeleteInternshipsByCompanyId(company.CompanyId);
                _repository.Company.DeleteCompany(company);
            }
        }

        private async Task DeleteStudentData(Student student)
        {
            if (student != null)
            {
                _repository.Student.DeleteStudent(student);
            }
        }




    }
}
