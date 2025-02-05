using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record SavedOpportunityDto
    {
        public string StudentId { get; set; }
        public int PostId { get; set; }
        public char Type
        {
            get; set;
        }
    }
}
