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

            CreateMap<User, UserDto>();


            CreateMap<Scholarship, ScholarshipDto>();

            CreateMap<University, UniversityViewDto>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                 .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));


            CreateMap<UserForRegistrationDto, User>();




            CreateMap<Scholarship, GetScholarshipDto>();
            CreateMap<ScholarshipForCreationDto, Scholarship>();
        
            
            CreateMap<University, UniversityDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
               .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.User.Bio))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));








        }
    }
}
