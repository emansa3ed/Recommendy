using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Student
{
    public record StudentDto
    {
        public string? UrlPicture { get; init; }
        public string? UserName { get; init; }
        public string? UrlResume { get; init; }
        public string? Bio { get; init; }
        public string? PhoneNumber { get; init; }

    }
    public record StudentForUpdateDto
    {

        public string? UserName { get; init; }
        public string? Bio { get; init; }
        public string? UrlResume { get; init; }
        public string? PhoneNumber { get; init; }
    }

    public record ChangePasswordDto
    {
        [Required(ErrorMessage = " CurrentPassword is  Required")]
        public string CurrentPassword { get; init; }
        [Required(ErrorMessage = " NewPassword is  Required")]
        public string NewPassword { get; init; }
    }
}
