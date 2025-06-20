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
		[StringLength(50, MinimumLength = 2, ErrorMessage = "FirstName must be between 2 and 50 characters")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "LastName is required")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "LastName must be between 2 and 50 characters")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Username is required")]
		[StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters")]
		[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Username can only contain English letters (A–Z, a–z)")]
		public string UserName { get; init; }
		[Required(ErrorMessage = "Password is required")]
		[StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters and include at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
		public string Password { get; init; }
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email format")]
		public string Email { get; init; }
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string? PhoneNumber { get; init; }
        [Required(ErrorMessage = "UserImage is required")]
        public IFormFile? UserImage { get; init; }

		public IFormFile? ResumeFile { get; set; }


		[Required(ErrorMessage = "Bio is required")]
		[StringLength(500, ErrorMessage = "Bio must not exceed 500 characters")]
		public string Bio { get; set; }
        public string? Url { get; set; }
        [Required(ErrorMessage = "Roles is required.")]
        public ICollection<string> Roles { get; init; }

    }
}
