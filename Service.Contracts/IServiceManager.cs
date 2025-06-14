namespace Service.Contracts
{
    public interface IServiceManager
    {
        IAdminService AdminService { get; }
        ICountryService CountryService { get; }
        IAuthenticationService AuthenticationService { get; }
        IUserService UserService { get; }
        IInternshipService InternshipService { get; }

        IInternshipPositionService InternshipPosition { get; }
        IPositionService PositionService { get; }

        IScholarshipService ScholarshipService { get; }
        IUniversityService UniversityService { get; }
        IEmailsService EmailsService { get; }
        IUserCodeService userCodeService { get; }

        IOpportunityService OpportunityService { get; }
        ICompanyService CompanyService { get; }
        IStudentService StudentService { get; }
		IFeedbackService FeedbackService { get; }
        IReportService ReportService { get; }   
        INotificationService NotificationService { get; }
        IChatUsersService ChatUsersService { get; }
        IChatMessageService ChatMessageService { get; }
		IResumeParserService ResumeParserService { get; }
		ISkillService  SkillService { get; }
        IProfileSuggestionService ProfileSuggestionService { get; }
        IOrganizationProfileService OrganizationProfileService { get; }
        IGeminiService GeminiService { get; }
        ISkillOntology SkillOntology { get; }
	}
}