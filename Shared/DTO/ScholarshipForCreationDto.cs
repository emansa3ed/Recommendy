using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Entities.Models;

namespace Shared.DTO
{
    public class ScholarshipForCreationDto
    {
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }
  
        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive number")]
        public decimal Cost { get; set; }

        [DataType(DataType.Date)]
        public DateTime ApplicationDeadline { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0")]
        public int Duration { get; set; }

        [EnumDataType(typeof(Degree))]
        public Degree Degree { get; set; }

        [EnumDataType(typeof(Funded))]
        public Funded Funded { get; set; }

        [Required(ErrorMessage = "University ID is required")]
        public string UniversityId { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
        public string Description { get; set; }

        public string Grants { get; set; }
        public string UrlApplicationForm { get; set; }

        public string? UrlPicture { get; set; }

        public string EligibleGrade { get; set; }

        public bool IsBanned { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //public Dictionary<string, string> Requirements { get; set; }
        public Dictionary<string, string> Requirements { get; set; } = new Dictionary<string, string>();

        public IFormFile ImageFile { get; set; }
    }
}