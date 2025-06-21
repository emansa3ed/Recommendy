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
using Service.Hubs;
using Service.Ontology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAdminService> _adminService;
        private readonly Lazy<ICountryService> _countryService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IInternshipService> _internshipService;
        private readonly Lazy<IInternshipPositionService> _internshipPositionService;
        private readonly Lazy<IPositionService> _positionService;
        private readonly Lazy<IScholarshipService> _scholarshipService;
        private readonly Lazy<IUniversityService> _universityService;
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
        private readonly Lazy<IChatMessageService> _chatMessageService;
        private readonly Lazy<IResumeParserService> _resumeParserService;
        private readonly Lazy<ISkillService> _skillService;
        private readonly Lazy<IProfileSuggestionService> _profileSuggestionService;
        private readonly Lazy<IOrganizationProfileService> _organizationProfileService;
        private readonly Lazy<IGeminiService> _geminiservice;
        private readonly Lazy<ISkillOntology> _skillOntology;
        private readonly Lazy<IOllamaService> _ollamaservice;
        private readonly Lazy<IQuestionClassificationService> _questionClassificationService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper,
            UserManager<User> userManager, IConfiguration configuration, 
            ILogger<ServiceManager> logger, 
            ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor, IEmailsService emailsService, HttpClient httpClient,
            IHubContext<NotificationHub> _hubContext, MyMemoryCache memoryCache, IResumeParserService resumeParserService, IHubContext<FeedbackHub> _FeedBackhubContext)
        {
            _logger = logger;
            _httpClient = httpClient;

            _adminService = new Lazy<IAdminService>(() => new AdminService(repositoryManager, mapper, emailsService, userManager, memoryCache));
            _countryService = new Lazy<ICountryService>(() => new CountryService(repositoryManager, mapper, memoryCache));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, userManager, configuration, repositoryManager, httpContextAccessor, userCodeService, resumeParserService, httpClient));
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, userManager, mapper, emailsService));
            _internshipService = new Lazy<IInternshipService>(() => new InternshipService(repositoryManager, mapper, memoryCache));
            _internshipPositionService = new Lazy<IInternshipPositionService>(() => new InternshipPositionService(repositoryManager, mapper));  
            _positionService = new Lazy<IPositionService>(() => new PositionService(repositoryManager));

            var scholarshipServiceLogger = loggerFactory.CreateLogger<ScholarshipService>();
            _scholarshipService = new Lazy<IScholarshipService>(() => new ScholarshipService(repositoryManager, mapper, scholarshipServiceLogger, memoryCache));
            _universityService = new Lazy<IUniversityService>(() => new UniversityService(repositoryManager, this, mapper, _notificationservice.Value, userManager));
            _emailsService = new Lazy<IEmailsService>(() => new EmailsService(configuration, loggerFactory.CreateLogger<EmailsService>(), repositoryManager, userManager));
            _userCodeService = new Lazy<IUserCodeService>(() => new UserCodeService(repositoryManager, emailsService, userManager));
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repositoryManager, mapper, _notificationservice.Value, this));
            _studentService = new Lazy<IStudentService>(() => new StudentService(repositoryManager, mapper, userManager));
            _notificationservice = new Lazy<INotificationService>(() => new NotificationService(repositoryManager, mapper, _hubContext, userManager));
            _feedbackService = new Lazy<IFeedbackService>(() => new FeedbackService(repositoryManager, mapper, _notificationservice.Value, _FeedBackhubContext));
            _reportService = new Lazy<IReportService>(() => new ReportService(repositoryManager, mapper, memoryCache));
            _opportunityService = new Lazy<IOpportunityService>(() => new OpportunityService(repositoryManager, mapper));
            _chatUsersService = new Lazy<IChatUsersService>(() => new ChatUsersService(repositoryManager));
            _chatMessageService = new Lazy<IChatMessageService>(() => new ChatMessageService(repositoryManager, mapper, _notificationservice.Value));
            _resumeParserService = new Lazy<IResumeParserService>(() => new ResumeParserService(repositoryManager));
            _skillService = new Lazy<ISkillService>(() => new SkillService(repositoryManager));
            _profileSuggestionService = new Lazy<IProfileSuggestionService>(() => new ProfileSuggestionService(repositoryManager, mapper, memoryCache, this));
            _organizationProfileService = new Lazy<IOrganizationProfileService>(() => new OrganizationProfileService(repositoryManager));
            _geminiservice = new Lazy<IGeminiService>(() => new GeminiService());
            _skillOntology = new Lazy<ISkillOntology>(() => new SkillOntology());
            _questionClassificationService = new Lazy<IQuestionClassificationService>(() => new QuestionClassificationService());
			_ollamaservice = new Lazy<IOllamaService>(() => new OllamaService(_httpClient, repositoryManager, _questionClassificationService.Value));

		}

        public IAdminService AdminService => _adminService.Value;
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
        public IChatMessageService ChatMessageService => _chatMessageService.Value;
        public IResumeParserService ResumeParserService => _resumeParserService.Value;
        public ISkillService SkillService => _skillService.Value;
        public IProfileSuggestionService ProfileSuggestionService => _profileSuggestionService.Value;
        public IOrganizationProfileService OrganizationProfileService => _organizationProfileService.Value;
        public IGeminiService GeminiService => _geminiservice.Value;
        public ISkillOntology SkillOntology => _skillOntology.Value;
        public IQuestionClassificationService QuestionClassificationService => _questionClassificationService.Value;

        public IOllamaService OllamaService => _ollamaservice.Value;
	}
}
