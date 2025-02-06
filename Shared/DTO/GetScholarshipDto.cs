using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record GetScholarshipDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Grants { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public string UrlApplicationForm { get; set; }
        public string UrlPicture { get; set; }
        public bool IsBanned { get; set; }
        public string EligibleGrade { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public string Degree { get; set; }
        public string Funded { get; set; }
        public string UniversityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Dictionary<string, string> Requirements { get; set; }
        public string UniversityUrl { get; set; }
        public string UserName { get; set; }
    }

}




