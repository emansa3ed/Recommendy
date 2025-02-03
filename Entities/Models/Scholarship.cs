using System.ComponentModel.DataAnnotations;
//using  Shared.Enums;
namespace Entities.Models
{
    public enum Funded
    {
        FullyFunded,
        Partially,
        Not
    }

    public enum Degree
    {
        Master,
        PhD,
        Bachelor
    }
    public class Scholarship
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Grants { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public string UrlApplicationForm { get; set; }
        public string UrlPicture { get; set; }
        public bool IsBanned { get; set; }
        public string EligibleGrade { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public Degree Degree { get; set; }
        public Funded Funded { get; set; }
        public string UniversityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Dictionary<string, string> Requirements { get; set; }


    }
}
