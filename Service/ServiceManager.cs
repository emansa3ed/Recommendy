using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICountryService> _countryService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IInternshipService> _internshipService;
        private readonly Lazy<IInternshipPositionService> _internshipPositionService;
        private readonly Lazy<IPositionService> _positionService;
        private readonly Lazy<IScholarshipService> _scholarshipService;
        private readonly Lazy< IUniversityService> _universityService;
        private readonly Lazy<IEmailsService> _emailsService;
        private readonly ILogger<ServiceManager> _logger;


        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager,IConfiguration configuration,  IWebHostEnvironment _webHostEnvironment, ILogger<ServiceManager> logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;

            _countryService = new Lazy<ICountryService>(() => new  CountryService(repositoryManager, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, userManager, configuration, repositoryManager));
           _userService = new Lazy<IUserService>(()=>  new UserService(repositoryManager,userManager ,mapper));
            _internshipService =    new Lazy<IInternshipService>(() =>  new InternshipService(repositoryManager , mapper));
            _internshipPositionService = new Lazy<IInternshipPositionService>(() => new InternshipPositionService(repositoryManager ,mapper));  
            _positionService = new Lazy<IPositionService>(()=> new PositionService(repositoryManager));

            var scholarshipServiceLogger = loggerFactory.CreateLogger<ScholarshipService>();
            _scholarshipService = new Lazy<IScholarshipService>(() => new ScholarshipService(repositoryManager, mapper,  scholarshipServiceLogger));
           _universityService  = new Lazy<IUniversityService>(() => new UniversityService(repositoryManager, mapper , userManager));
            _emailsService = new Lazy<IEmailsService>(() => new EmailsService(configuration));
        }



        public ICountryService CountryService => _countryService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IUserService UserService => _userService.Value;
        public IInternshipService InternshipService => _internshipService.Value;
        public IInternshipPositionService InternshipPosition => _internshipPositionService.Value;
       
        public IPositionService PositionService => _positionService.Value;

        public IScholarshipService ScholarshipService => _scholarshipService.Value;

        public IUniversityService UniversityService => _universityService.Value;

        public IEmailsService EmailsService => _emailsService.Value;
    }
}
