using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Company
{
    public record CompanyDto
    {

        public string? CompanyUrl { get; init; }
        public string? UserName { get; init; }
        public string? Bio { get; init; }
        public string? PhoneNumber { get; init; }

    }
    public record CompanyViewDto
    {
        public string? CompanyUrl { get; init; }
        public string? UrlPicture { get; init; }
        public string? UserName { get; init; }
        public string? Bio { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
