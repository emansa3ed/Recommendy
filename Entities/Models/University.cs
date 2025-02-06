using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class University
    {
        [Key]
        public string UniversityId { get; set; }
        public int? CountryId { get; set; }
        public bool Revised { get; set; } = true;
        public string UniversityUrl { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

        public virtual ICollection<Scholarship> Scholarships { get; set; }


    }
}
