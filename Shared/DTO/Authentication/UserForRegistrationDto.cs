using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Authentication
{
    public record UserForRegistrationDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; init; }
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string? PhoneNumber { get; init; }
        [Required(ErrorMessage = "UserImage is required")]
        public IFormFile? UserImage { get; init; }
        [Required(ErrorMessage = "Bio is required")]

        public string Bio { get; set; }
        public string? Url { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public ICollection<string> Roles { get; init; }

    }
}
