using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.University
{
    public record UniversityViewDto
    {
        public string? UniversityUrl { get; init; }
        public string? UrlPicture { get; init; }
        public string? UserName { get; init; }
        public int? CountryId { get; init; }
        public string? CountryName { get; init; }
        public string? Bio { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
