using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Student 
    {
        [Key]
        public string StudentId { get; set; }
        [Required]
        public string UrlResume { get; set; }
    
       
    

    }

}
