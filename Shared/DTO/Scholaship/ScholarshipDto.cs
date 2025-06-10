using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Http;
//using Shared.Enums;

namespace Shared.DTO.Scholaship
{
    public record ScholarshipDto
    {
        [MinLength(5, ErrorMessage = "Name must be at least 5 characters long")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string? Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive number")]
        public decimal? Cost { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ApplicationDeadline { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 day")]
        public int? Duration { get; set; }

        [EnumDataType(typeof(Degree))]
        public Degree? Degree { get; set; }

        [EnumDataType(typeof(Funded))]
        public Funded? Funded { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
        public string? Description { get; set; }

        public string? Grants { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        public string? UrlApplicationForm { get; set; }

        public string? EligibleGrade { get; set; }
        public string? Requirements { get; set; }
        public IFormFile? ImageFile { get; set; }
    }

    public record EnumDto(int Id, string Name);
    public record EditScholarshipProfileDto
    {
        public GetScholarshipDto Scholarship { get; init; }
        public string FundedOptions { get; init; }
        public string DegreeOptions { get; init; }
    };
}
