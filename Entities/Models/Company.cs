using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Company
    {
        [Key]
        public string CompanyId { get; set; }
        
        public string? CompanyUrl { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

		[JsonIgnore]
		public virtual ICollection<Internship> Internships { get; set; }

    }
}
