using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Student 
    {
        [Key]
        public string StudentId { get; set; }
        public string ? UrlResume { get; set; }

        public User User { get; set; }



    }

}
