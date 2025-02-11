using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Admin
    {
        [Key]
        public string AdminId { get; set; }

        public User User { get; set; }

       
    }
}
