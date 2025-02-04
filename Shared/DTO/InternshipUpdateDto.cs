using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
   public record InternshipUpdateDto
    {

       
        public string? Name { get; set; }

      
        public string? UrlApplicationForm { get; set; }

       
        public DateTime? ApplicationDeadline { get; set; }

      
        public string? Description { get; set; }

        
        public bool? Paid { get; set; }

        public string? Approach { get; set; }
        public IFormFile? Image { get; init; }

    }
}
