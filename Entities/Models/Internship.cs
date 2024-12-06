using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
   
       public enum Approach
        {
            OnSite,
            Hybrid,
            Online
        }

        public class Internship
        {
            [Key]
            public int Id { get; set; }

            public string Name { get; set; }

        
            public string UrlApplicationForm { get; set; }

           
            public DateTime ApplicationDeadline { get; set; }

            public string Description { get; set; }

          
            public string UrlPicture { get; set; }

            public bool IsBanned { get; set; }

        
            public string  CompanyId { get; set; }

            public DateTime CreatedAt { get; set; }

          
            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }

            public bool Paid { get; set; }

          
            public Approach Approach { get; set; }
        }
    }



