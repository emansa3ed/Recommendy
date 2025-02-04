using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class InternshipPosition
    {
        public int InternshipId { get; set; }
        public int PositionId { get; set; }
        public string Requirements { get; set; }
        public int NumOfOpenings { get; set; }
        
        [JsonIgnore]
        public virtual Internship Internship { get; set; }
        public virtual Position Position { get; set; }




    }
}
