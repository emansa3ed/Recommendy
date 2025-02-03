using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record UniversityDto
    {
        
        public string? UniversityUrl { get; init; }
        public string? UserName { get; init; }
        public int? CountryId { get; init; }
        public string? Bio { get; init; }
        public string? PhoneNumber { get; init; }
        public string? CurrentPassword { get; init; } 
        public string? NewPassword { get; init; }
    }
}
