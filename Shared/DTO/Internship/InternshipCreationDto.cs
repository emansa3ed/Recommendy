using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Internship
{
    public record InternshipCreationDto
    {


        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Application form URL is required")]
        [Url(ErrorMessage = "Invalid URL format")]
        public string? UrlApplicationForm { get; set; }

        [Required(ErrorMessage = "Application deadline is required")]
        public DateTime? ApplicationDeadline { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        public string? CompanyId { get; set; }

        [Required(ErrorMessage = "Paid status is required")]
        public bool? Paid { get; set; }

        [Required(ErrorMessage = "Approach is required")]
        public string? Approach { get; set; }
        public IFormFile? Image { get; init; }



    }
}
