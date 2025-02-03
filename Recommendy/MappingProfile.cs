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
          .ForMember(dest => dest.Positions, opt => opt.MapFrom(src => src.InternshipPositions));
            CreateMap<InternshipPosition, InternshipPositionDto>();

            CreateMap<User, UserDto>();




        }
    }
}
