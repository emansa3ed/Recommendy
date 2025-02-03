using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Models
{
   
    

        public class Internship
        {
            [Key]
            public int Id { get; set; }

            public string Name { get; set; }

        
            public string UrlApplicationForm { get; set; }

           
            public DateTime ApplicationDeadline { get; set; }

            public string Description { get; set; }

          
            public string ? UrlPicture { get; set; }

            public bool? IsBanned { get; set; }=false;
 
        public string  CompanyId { get; set; }

            public DateTime? CreatedAt { get; set; }= DateTime.Now;

            public bool Paid { get; set; }

          
            public string  Approach { get; set; }

      
        public virtual ICollection<InternshipPosition>? InternshipPositions { get; set; } = new List<InternshipPosition>();
      
    }


}



