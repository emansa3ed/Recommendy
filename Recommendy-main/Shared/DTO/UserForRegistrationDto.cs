using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record UserForRegistrationDto
    {
      
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }
        public string Email { get; init; }
        public string? PhoneNumber { get; init; }
        public IFormFile? UserImage { get; init; }
        public string? Bio { get; set; }
        public string? Url { get; set; } 
        [Required(ErrorMessage = "Role is required.")]
        public ICollection<string> Roles { get; init; }

    }
}
