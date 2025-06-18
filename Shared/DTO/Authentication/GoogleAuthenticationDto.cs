using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Authentication
{
    public class GoogleAuthenticationDto
    {

        [Required(ErrorMessage = "IdToken is required")]
        public string IdToken { get; set; }
       
    }
} 