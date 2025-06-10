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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.DTO.Admin;

namespace Service
{
    internal sealed class AdminService : IAdminService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IEmailsService _emailsService;
        private readonly UserManager<User> _userManager;
        private readonly MyMemoryCache _cache;
        private const string DASHBOARD_STATS_CACHE_KEY = "AdminDashboard_Stats";
        private const int CACHE_SIZE = 1024 * 1024; 
        private readonly TimeSpan CACHE_DURATION = TimeSpan.FromMinutes(0.5);
        public AdminService(IRepositoryManager repository, IMapper mapper, IEmailsService emailsService, UserManager<User> userManager, MyMemoryCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _emailsService = emailsService;
            _userManager = userManager;
            _cache = cache;
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


            var (subject, message) = EmailTemplates.Account.GetBanTemplate(banDto.Reason);
            await _emailsService.Sendemail(user.Email, message, subject);
        }

        public async Task UnbanUserAsync(string userId, bool trackChanges)
        {
            var user = await _repository.Admin.GetUserByIdAsync(userId, trackChanges);
            if (user is null)
                throw new UserNotFoundException(userId);

            user.IsBanned = false;
            await _repository.SaveAsync();

            var (subject, message) = EmailTemplates.Account.GetUnbanTemplate();
            await _emailsService.Sendemail(user.Email, message, subject);
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




        
    
            public async Task<AdminDashboardStatsDto> GetDashboardStatisticsAsync()
            {
                if (_cache.Cache.TryGetValue(DASHBOARD_STATS_CACHE_KEY, out AdminDashboardStatsDto cachedStats))
                {
                    return cachedStats;
                }
                var stats = await CalculateDashboardStatisticsAsync();   
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSize(CACHE_SIZE)
                    .SetAbsoluteExpiration(CACHE_DURATION);

                _cache.Cache.Set(DASHBOARD_STATS_CACHE_KEY, stats, cacheEntryOptions);

                return stats;
            }

        private async Task<AdminDashboardStatsDto> CalculateDashboardStatisticsAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var companyParams = new CompanyParameters { PageSize = int.MaxValue };
            var companies = await _repository.Company
                .GetAllCompaniesAsync(companyParams, false);

            var universityParams = new UniversityParameters { PageSize = int.MaxValue };
            var universities = await _repository.university
                .GetAllUniversitiesAsync(universityParams, false);

            var internshipParams = new InternshipParameters { PageSize = int.MaxValue };
            var internships = await _repository.Intership
                .GetAllInternshipsAsync(internshipParams, false);

            var scholarshipParams = new ScholarshipsParameters { PageSize = int.MaxValue };
            var scholarships = await _repository.Scholarship
                .GetAllScholarshipsAsync(scholarshipParams, false);

            return new AdminDashboardStatsDto
            {
              
                Users = _mapper.Map<UserStatistics>(users),
                Organizations = new OrganizationStatistics
                {
                    Companies = _mapper.Map<CompanyAnalytics>((Companies: companies, Internships: internships)),
                    Universities = new UniversityAnalytics
                    {
                        TotalUniversities = universities.Count,
                        VerifiedUniversities = universities.Count(u => u.IsVerified),
                        UnverifiedUniversities = universities.Count(u => !u.IsVerified),
                        ScholarshipMetrics = new ScholarshipMetrics
                        {
                            UniversitiesWithScholarships = scholarships
                                .Select(s => s.UniversityId)
                                .Distinct()
                                .Count()
                        }
                    }
                },
                Opportunities = new OpportunityStatistics
                {
                    Internships = _mapper.Map<InternshipStatistics>(internships),
                    Scholarships = _mapper.Map<ScholarshipStatistics>(scholarships)
                }
            };
        }


    }
        }


    

