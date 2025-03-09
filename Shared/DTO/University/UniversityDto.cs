using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO.Country;

namespace Shared.DTO.University
{
    public record UniversityDto
    {

        public string? UniversityUrl { get; init; }
        public string? UserName { get; init; }
        public int? CountryId { get; init; }
        public string? Bio { get; init; }
        public string? PhoneNumber { get; init; }


    }
    public record EditUniversityProfileDto
    {
        public string? UniversityUrl { get; init; }
        public string? UserName { get; init; }
        public int? CountryId { get; init; }
        public string? Bio { get; init; }
        public string? PhoneNumber { get; init; }
        public IEnumerable<CountryDto>? Countries { get; init; }

    }
}
