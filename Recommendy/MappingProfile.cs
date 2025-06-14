using AutoMapper;
using Entities.Models;
using Shared.DTO.Admin;
using Shared.DTO.Authentication;
using Shared.DTO.Chat;
using Shared.DTO.Company;
using Shared.DTO.Country;
using Shared.DTO.Feedback;
using Shared.DTO.Internship;
using Shared.DTO.Notification;
using Shared.DTO.opportunity;
using Shared.DTO.Report;
using Shared.DTO.Scholaship;
using Shared.DTO.Student;
using Shared.DTO.University;
using Shared.DTO.User;
using Shared.RequestFeatures;

namespace Recommendy
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

        CreateMap<ChatUsers, ChatDto>()
                .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src => src.Messages.FirstOrDefault()));
           


            CreateMap<Country, CountryDto>();
            CreateMap<UserForRegistrationDto, User>();

            CreateMap<User, UserDto>()
                    .ForMember(dest => dest.Discriminator, opt =>
                        opt.MapFrom(src => src.Discriminator))
                    .ForMember(dest => dest.UniversityUrl, opt =>
                        opt.MapFrom(src => src.University != null ? src.University.UniversityUrl : null))
                    .ForMember(dest => dest.CompanyUrl, opt =>
                        opt.MapFrom(src => src.Company != null ? src.Company.CompanyUrl : null))
                    .ForMember(dest => dest.CountryName, opt =>
                        opt.MapFrom(src => src.University != null ? src.University.Country.Name : null))
                    .ForMember(dest => dest.IsVerified, opt =>
                        opt.MapFrom(src =>
                            src.University != null ? src.University.IsVerified :
                            src.Company != null ? src.Company.IsVerified : false));

        
            CreateMap<InternshipCreationDto, Internship>();
            CreateMap<InternshipPositionDto, InternshipPosition>();
            CreateMap<ChatMessage, ChatMessageDto>();
                

            CreateMap<Internship, InternshipDto>()
              .ForMember(dest => dest.Positions, opt => opt.MapFrom(src => src.InternshipPositions))
              .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));

            CreateMap<InternshipPosition, InternshipPositionViewDto>()
                   .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.Name));

            CreateMap<Position, PositionDto>();



            CreateMap<Scholarship, ScholarshipDto>();

            CreateMap<NotificationCreationDto, Notification>();
			CreateMap<Notification, NotificationDto>();

			CreateMap<ReportDtoCreation, Report>();
            CreateMap<Report, ReportDto>();

            CreateMap<FeedbackCreationDto, Feedback>();
            CreateMap<Feedback, FeedBackDto>();

			CreateMap<ScholarshipDto, Scholarship>()
	        .ForMember(dest => dest.Name, opt => opt.MapFrom((src, dest) => src.Name ?? dest.Name))
	        .ForMember(dest => dest.Cost, opt => opt.MapFrom((src, dest) => src.Cost ?? dest.Cost))
	        .ForMember(dest => dest.ApplicationDeadline, opt => opt.MapFrom((src, dest) => src.ApplicationDeadline ?? dest.ApplicationDeadline))
	        .ForMember(dest => dest.StartDate, opt => opt.MapFrom((src, dest) => src.StartDate ?? dest.StartDate))
	        .ForMember(dest => dest.Duration, opt => opt.MapFrom((src, dest) => src.Duration ?? dest.Duration))
	        .ForMember(dest => dest.Degree, opt => opt.MapFrom((src, dest) => src.Degree ?? dest.Degree))
	        .ForMember(dest => dest.Funded, opt => opt.MapFrom((src, dest) => src.Funded ?? dest.Funded))
	        .ForMember(dest => dest.Description, opt => opt.MapFrom((src, dest) => src.Description ?? dest.Description))
	        .ForMember(dest => dest.Grants, opt => opt.MapFrom((src, dest) => src.Grants ?? dest.Grants))
	        .ForMember(dest => dest.UrlApplicationForm, opt => opt.MapFrom((src, dest) => src.UrlApplicationForm ?? dest.UrlApplicationForm))
	        .ForMember(dest => dest.EligibleGrade, opt => opt.MapFrom((src, dest) => src.EligibleGrade ?? dest.EligibleGrade))
	        .ForMember(dest => dest.Requirements, opt => opt.MapFrom((src, dest) => src.Requirements ?? dest.Requirements))
	        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


			CreateMap<University, UniversityViewDto>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                 .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.UrlPicture, opt => opt.MapFrom(src => src.User.UrlPicture));



            CreateMap<UserForRegistrationDto, User>();


            CreateMap<Scholarship, EditedScholarshipDto>()
             .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Degree.ToString())) 
             .ForMember(dest => dest.Funded, opt => opt.MapFrom(src => src.Funded.ToString())); 

            CreateMap<Scholarship, GetScholarshipDto>()
             .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Degree.ToString())) 
             .ForMember(dest => dest.Funded, opt => opt.MapFrom(src => src.Funded.ToString()))  
             .ForMember(dest => dest.University, opt => opt.MapFrom(src => src.University));


            CreateMap<ScholarshipForCreationDto, Scholarship>();



            CreateMap<University, UniversityDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
               .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));


            CreateMap<Company, CompanyVerificationDto>()
           .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.IsVerified))
           .ForMember(dest => dest.VerificationNotes, opt => opt.MapFrom(src => src.VerificationNotes)); 

            CreateMap<University, UniversityVerificationDto>()
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.IsVerified))
                .ForMember(dest => dest.VerificationNotes, opt => opt.MapFrom(src => src.VerificationNotes));
               



           // CreateMap<User, UserDto>()
            // .ForMember(dest => dest.CompanyUrl, opt => opt.MapFrom(src => src.Company.CompanyUrl))
            //// .ForMember(dest => dest.UniversityUrl, opt => opt.MapFrom(src => src.University.UniversityUrl));

            CreateMap<SavedOpportunityDto, SavedPost>();



            /////////////////////////////
            CreateMap<Student, StudentDto>()
      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
      .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
      .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
      .ForMember(dest => dest.UrlPicture, opt => opt.MapFrom(src => src.User.UrlPicture))
      .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));


			CreateMap<Student, StudentForUpdateDto>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                 .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));
            CreateMap<University, UniversityViewDto>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                 .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                 .ForMember(dest => dest.UrlPicture, opt => opt.MapFrom(src => src.User.UrlPicture))
                 .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.IsVerified));
           
            CreateMap<Company, CompanyViewDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.UrlPicture, opt => opt.MapFrom(src => src.User.UrlPicture))
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.IsVerified)); 


            CreateMap<Company, CompanyDto>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
              .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
              .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));




                // Admin Dashboard Statistics Mappings

                CreateMap<IEnumerable<User>, UserStatistics>()
                    .ForMember(dest => dest.TotalUsers, opt => opt.MapFrom(src => src.Count()))
                    .ForMember(dest => dest.ActiveUsers, opt => opt.MapFrom(src => src.Count(u => !u.IsBanned)))
                    .ForMember(dest => dest.BannedUsers, opt => opt.MapFrom(src => src.Count(u => u.IsBanned)))
                    .ForMember(dest => dest.UserTypes, opt => opt.MapFrom(src => new UserTypeDistribution
                    {
                        Students = new UserTypeStats
                        {
                            Total = src.Count(u => u.Discriminator == "Student"),
                            Active = src.Count(u => u.Discriminator == "Student" && !u.IsBanned),
                            Banned = src.Count(u => u.Discriminator == "Student" && u.IsBanned)
                        },
                        Companies = new UserTypeStats
                        {
                            Total = src.Count(u => u.Discriminator == "Company"),
                            Active = src.Count(u => u.Discriminator == "Company" && !u.IsBanned),
                            Banned = src.Count(u => u.Discriminator == "Company" && u.IsBanned)
                        },
                        Universities = new UserTypeStats
                        {
                            Total = src.Count(u => u.Discriminator == "University"),
                            Active = src.Count(u => u.Discriminator == "University" && !u.IsBanned),
                            Banned = src.Count(u => u.Discriminator == "University" && u.IsBanned)
                        },
                        Admins = new UserTypeStats
                        {
                            Total = src.Count(u => u.Discriminator == "Admin"),
                            Active = src.Count(u => u.Discriminator == "Admin" && !u.IsBanned),
                            Banned = src.Count(u => u.Discriminator == "Admin" && u.IsBanned)
                        }
                    }));

            CreateMap<(PagedList<Company> Companies, PagedList<Internship> Internships), CompanyAnalytics>()
        .ForMember(dest => dest.TotalCompanies, opt =>
            opt.MapFrom(src => src.Companies.Count))
        .ForMember(dest => dest.VerifiedCompanies, opt =>
            opt.MapFrom(src => src.Companies.Count(c => c.IsVerified)))
        .ForMember(dest => dest.UnverifiedCompanies, opt =>
            opt.MapFrom(src => src.Companies.Count(c => !c.IsVerified)))
        .ForMember(dest => dest.InternshipMetrics, opt =>
            opt.MapFrom(src => new InternshipMetrics
            {
                CompaniesWithInternships = src.Internships
                    .Select(i => i.CompanyId)
                    .Distinct()
                    .Count()
            }));
            // University Analytics Mappings
            CreateMap<(PagedList<University> Universities, PagedList<Scholarship> Scholarships), UniversityAnalytics>()
           .ForMember(dest => dest.TotalUniversities, opt =>
               opt.MapFrom(src => src.Universities.Count))
           .ForMember(dest => dest.VerifiedUniversities, opt =>
               opt.MapFrom(src => src.Universities.Count(u => u.IsVerified)))
           .ForMember(dest => dest.UnverifiedUniversities, opt =>
               opt.MapFrom(src => src.Universities.Count(u => !u.IsVerified)))
           .ForMember(dest => dest.ScholarshipMetrics, opt =>
               opt.MapFrom(src => new ScholarshipMetrics
               {
                   UniversitiesWithScholarships = src.Scholarships
                       .Select(s => s.UniversityId)
                       .Distinct()
                       .Count()
               }));

            // Internship Statistics Mappings
            CreateMap<IEnumerable<Internship>, InternshipStatistics>()
       .ForMember(dest => dest.General, opt => opt.MapFrom(src => new GeneralMetrics
       {
           Total = src.Count(),
           Active = src.Count(i => i.IsBanned == false),
           Banned = src.Count(i => i.IsBanned == true)
       }))
       .ForMember(dest => dest.Timeline, opt => opt.MapFrom(src => new TimeBasedMetrics
       {
           OpenForApplications = src.Count(i => i.ApplicationDeadline > DateTime.UtcNow),
           ClosedApplications = src.Count(i => i.ApplicationDeadline <= DateTime.UtcNow),
           StartingThisMonth = src.Count(i => i.CreatedAt.HasValue && i.CreatedAt.Value.Month == DateTime.UtcNow.Month),
           EndingThisMonth = src.Count(i => i.ApplicationDeadline.Month == DateTime.UtcNow.Month)
       }))
       .ForMember(dest => dest.Compensation, opt => opt.MapFrom(src => new CompensationMetrics
       {
           PaidOpportunities = src.Count(i => i.Paid),
           UnpaidOpportunities = src.Count(i => !i.Paid),
           PaidPercentage = src.Any() ? src.Count(i => i.Paid) * 100m / src.Count() : 0
       }))
       .ForMember(dest => dest.PopularPositions, opt => opt.MapFrom(src =>
           src.Where(i => i.InternshipPositions != null && i.InternshipPositions.Any())
              .SelectMany(i => i.InternshipPositions)
              .GroupBy(p => p.Position.Name)
              .ToDictionary(g => g.Key, g => g.Count())));

            // Compensation Metrics Mapping (separate to handle the complex calculation)
            CreateMap<IEnumerable<Internship>, CompensationMetrics>()
                    .ForMember(dest => dest.PaidOpportunities, opt => opt.MapFrom(src => src.Count(i => i.Paid)))
                    .ForMember(dest => dest.UnpaidOpportunities, opt => opt.MapFrom(src => src.Count(i => !i.Paid)))
                    .ForMember(dest => dest.PaidPercentage, opt => opt.MapFrom(src =>
                        src.Any() ? src.Count(i => i.Paid) * 100m / src.Count() : 0));

                // Scholarship Statistics Mappings
                CreateMap<IEnumerable<Scholarship>, ScholarshipStatistics>()
                    .ForMember(dest => dest.General, opt => opt.MapFrom(src => new GeneralMetrics
                    {
                        Total = src.Count(),
                        Active = src.Count(s => !s.IsBanned),
                        Banned = src.Count(s => s.IsBanned)
                    }))
                    .ForMember(dest => dest.Timeline, opt => opt.MapFrom(src => new TimeBasedMetrics
                    {
                        OpenForApplications = src.Count(s => s.ApplicationDeadline > DateTime.UtcNow),
                        ClosedApplications = src.Count(s => s.ApplicationDeadline <= DateTime.UtcNow),
                        StartingThisMonth = src.Count(s => s.StartDate.Month == DateTime.UtcNow.Month),
                        EndingThisMonth = src.Count(s => s.StartDate.AddMonths(s.Duration).Month == DateTime.UtcNow.Month)
                    }))
                    .ForMember(dest => dest.Funding, opt => opt.MapFrom(src => new FundingMetrics
                    {
                        FullyFunded = src.Count(s => s.Funded == Funded.FullyFunded),
                        PartiallyFunded = src.Count(s => s.Funded == Funded.Partially),
                        Unfunded = src.Count(s => s.Funded == Funded.Not),
                        AverageCostByDegree = src.GroupBy(s => s.Degree.ToString())
                            .ToDictionary(g => g.Key, g => g.Average(s => s.Cost))
                    }))
                    .ForMember(dest => dest.DegreeDistribution, opt => opt.MapFrom(src =>
                        src.GroupBy(s => s.Degree.ToString())
                           .ToDictionary(g => g.Key, g => g.Count())));

                // Metrics Mappings
                CreateMap<IEnumerable<Internship>, InternshipMetrics>()
                    .ForMember(dest => dest.CompaniesWithInternships, opt => opt.MapFrom(src =>
                        src.Select(i => i.CompanyId).Distinct().Count()));

                CreateMap<IEnumerable<Scholarship>, ScholarshipMetrics>()
                    .ForMember(dest => dest.UniversitiesWithScholarships, opt => opt.MapFrom(src =>
                        src.Select(s => s.UniversityId).Distinct().Count()));
         
       

        CreateMap<DateTime, string>()
                    .ConvertUsing(dt => dt.ToString("yyyy-MM-dd HH:mm:ss"));

        CreateMap<User, SenderDto>()
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.UrlPicture));
        }
    }
}
    
