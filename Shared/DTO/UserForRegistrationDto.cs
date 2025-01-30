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
        [Required(ErrorMessage =" Photo is required")]

        public string? UrlPicture { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Bio { get; set; }
        public ICollection<string> Roles { get; init; }
    }
}
