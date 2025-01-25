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


        }
    }
}
