using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Company
    {
        [Key]
        public string CompanyId { get; set; }
        [Required]
        public string CompanyUrl { get; set; }
       
    }
}
