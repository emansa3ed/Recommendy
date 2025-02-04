using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record InternshipDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlApplicationForm { get; set; }
        public string ApplicationDeadline { get; set; }
        public string Description { get; set; }
        public string UrlPicture { get; set; }
        public bool Paid { get; set; }
        public string Approach { get; set; }

        public List<InternshipPositionViewDto> Positions { get; set; }
    }
}
