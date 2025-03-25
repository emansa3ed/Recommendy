using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly Lazy<IUserCodeService> _userCodeService;
        private readonly Lazy<IOpportunityService> _opportunityService;
        private readonly Lazy<IStudentService> _studentService;
        private readonly Lazy<ICompanyService> _companyService;
        private readonly ILogger<ServiceManager> _logger;
		private readonly HttpClient _httpClient; 
		private readonly Lazy<IFeedbackService> _feedbackService;
        private readonly Lazy<IReportService> _reportService;
        private readonly Lazy<INotificationService> _notificationservice;

        private readonly Lazy<IChatUsersService> _chatUsersService;


		public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper,
            UserManager<User> userManager,IConfiguration configuration, 
            IWebHostEnvironment _webHostEnvironment, ILogger<ServiceManager> logger, 
            ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor, IEmailsService emailsService, IUserCodeService userCodeService,HttpClient httpClient,
            IHubContext<NotificationHub> _hubContext, MyMemoryCache memoryCache)
        {
            _logger = logger;

            _countryService = new Lazy<ICountryService>(() => new  CountryService(repositoryManager, mapper, memoryCache));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, userManager, configuration, repositoryManager ,httpContextAccessor , userCodeService,httpClient));
           _userService = new Lazy<IUserService>(()=>  new UserService(repositoryManager,userManager ,mapper , emailsService));
            _internshipService =    new Lazy<IInternshipService>(() =>  new InternshipService(repositoryManager , mapper, memoryCache));
            _internshipPositionService = new Lazy<IInternshipPositionService>(() => new InternshipPositionService(repositoryManager ,mapper));  
            _positionService = new Lazy<IPositionService>(()=> new PositionService(repositoryManager));

            var scholarshipServiceLogger = loggerFactory.CreateLogger<ScholarshipService>();
            _scholarshipService = new Lazy<IScholarshipService>(() => new ScholarshipService(repositoryManager, mapper,  scholarshipServiceLogger, memoryCache));
           _universityService  = new Lazy<IUniversityService>(() => new UniversityService(repositoryManager, mapper , userManager));
            _emailsService = new Lazy<IEmailsService>(() => new EmailsService(configuration , repositoryManager , userManager));
            _userCodeService = new Lazy<IUserCodeService>(() => new UserCodeService(repositoryManager, emailsService,userManager));
            _companyService = new Lazy<ICompanyService>(() => new CompanyService( repositoryManager ,mapper ,userManager));
            _studentService = new Lazy<IStudentService>(() => new StudentService(repositoryManager ,mapper , userManager));
			_feedbackService = new Lazy<IFeedbackService>(() => new FeedbackService(repositoryManager, mapper));
            _reportService = new Lazy<IReportService>(() => new ReportService( repositoryManager , mapper, memoryCache));
			_notificationservice = new Lazy<INotificationService>(() => new NotificationService( repositoryManager , mapper, _hubContext, userManager));
            _opportunityService = new Lazy<IOpportunityService>(() =>  new OpportunityService(repositoryManager , mapper,_notificationservice.Value));
             _chatUsersService = new Lazy<IChatUsersService>(() => new ChatUsersService(repositoryManager ));


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
        public IUserCodeService userCodeService => _userCodeService.Value;

        public IOpportunityService OpportunityService => _opportunityService.Value;

        public IStudentService StudentService => _studentService.Value;

        public ICompanyService CompanyService => _companyService.Value;

		public IFeedbackService FeedbackService => _feedbackService.Value;
        public IReportService ReportService => _reportService.Value;    
        public INotificationService NotificationService => _notificationservice.Value;

        public IChatUsersService ChatUsersService => _chatUsersService.Value;


    }
}
