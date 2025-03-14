using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class University
    {
        [Key]
        public string UniversityId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public int? CountryId { get; set; }
        public bool Revised { get; set; } = true;
        public string UniversityUrl { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
       
        public virtual Country Country { get; set; }

		[JsonIgnore]
		public virtual ICollection<Scholarship> Scholarships { get; set; }


    }
}
