using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Internship
{
    public record InternshipPositionViewDto
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public string Requirements { get; set; }
        public int NumOfOpenings { get; set; }
    }
}
