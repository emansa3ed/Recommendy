using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record UserDto
    {

        public string Id { get; set; }           
        public string UserName { get; set; }    
        public string Email { get; set; }     
        public string PhoneNumber { get; set; }
        public string? Discriminator { get; set; }

        public string CompanyUrl { get; set; }
        public string UniversityUrl { get; set; }
        public string? UrlPicture { get; set; }
        public string? Bio { get; set; }


    }
}
