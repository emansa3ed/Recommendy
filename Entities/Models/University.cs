using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Entities.Models
{
    public class University 
    {
        [Key]
        public string UniversityId { get; set; }
        public int CountryId { get; set; }
     
       
    }
}
