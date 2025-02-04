using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class User : IdentityUser
    {
       
        public string ?UrlPicture { get; set; }
        public string ?Bio { get; set; }
        public string? Discriminator { get; set; }
        public bool IsBanned { get; set; }=false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    
        public University University { get; set; }
        public Company Company { get; set; }
        public Student Student { get; set; }





    }
}
