using Entities.Models;
using Shared.DTO.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Internship
{
    public record InternshipDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlApplicationForm { get; set; }
        public string ApplicationDeadline { get; set; }
        public string Description { get; set; }
        public string UrlPicture { get; set; }
        public bool IsBanned { get; set; } 
        public DateTime? CreatedAt { get; set; }
        public bool Paid { get; set; }
        public string Approach { get; set; }

        public CompanyDto Company { get; set; }

        public List<InternshipPositionViewDto> Positions { get; set; }
    }
}
