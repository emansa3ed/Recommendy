using AutoMapper;
using Entities.Models;
using Shared.DTO;

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

            CreateMap<Internship, InternshipDto>()
              .ForMember(dest => dest.Positions, opt => opt.MapFrom(src => src.InternshipPositions))
              .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.User.UserName));

            CreateMap<InternshipPosition, InternshipPositionViewDto>()
                   .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.Name));

            CreateMap<Position, PositionDto>();



            CreateMap<Scholarship, ScholarshipDto>();

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
             .ForMember(dest => dest.UniversityUrl, opt => opt.MapFrom(src => src.University.UniversityUrl))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.University.User.UserName));


            CreateMap<ScholarshipForCreationDto, Scholarship>();



            CreateMap<University, UniversityDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
               .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));




            CreateMap<User, UserDto>()
             .ForMember(dest => dest.CompanyUrl, opt => opt.MapFrom(src => src.Company.CompanyUrl))
             .ForMember(dest => dest.UniversityUrl, opt => opt.MapFrom(src => src.University.UniversityUrl));



            /////////////////////////////
            CreateMap<Student, StudentDto>()
      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
      .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
      .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
      .ForMember(dest => dest.UrlPicture, opt => opt.MapFrom(src => src.User.UrlPicture));


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
