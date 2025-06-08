using AutoMapper;
using Entities.Models;
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

namespace Recommendy
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Country, CountryDto>();
            CreateMap<UserForRegistrationDto, User>();
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




            CreateMap<User, UserDto>()
             .ForMember(dest => dest.CompanyUrl, opt => opt.MapFrom(src => src.Company.CompanyUrl))
             .ForMember(dest => dest.UniversityUrl, opt => opt.MapFrom(src => src.University.UniversityUrl));

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
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                 .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                 .ForMember(dest => dest.UrlPicture, opt => opt.MapFrom(src => src.User.UrlPicture));
            CreateMap<Company, CompanyViewDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.UrlPicture, opt => opt.MapFrom(src => src.User.UrlPicture));


            CreateMap<Company, CompanyDto>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
              .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
              .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));




        }
    }
}
