using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string UrlPicture { get; set; }
        public string name { get; set; }
        public string Bio { get; set; }
        public string Discriminator { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreatedAt { get; set; }

        
  


    }
}
