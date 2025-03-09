using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Authentication
{
    public record ForgotPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
