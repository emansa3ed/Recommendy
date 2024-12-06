using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Student 
    {
        [Key]
        public string StudentId { get; set; }
        public string UrlResume { get; set; }
        public string Interests { get; set; }
       
    

    }

}
