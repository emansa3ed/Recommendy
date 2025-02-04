using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Entities.Models;

namespace Shared.DTO
{
    public class ScholarshipForCreationDto
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }

        [DataType(DataType.Date)]
        public DateTime ApplicationDeadline { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }

        [EnumDataType(typeof(Degree))]
        public Degree Degree { get; set; }

        [EnumDataType(typeof(Funded))]
        public Funded Funded { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
        public string Description { get; set; }

        public string Grants { get; set; }
        public string UrlApplicationForm { get; set; }

        public string EligibleGrade { get; set; }

        public Dictionary<string, string> Requirements { get; set; } = new Dictionary<string, string>();

        public IFormFile ImageFile { get; set; }
    }
}
