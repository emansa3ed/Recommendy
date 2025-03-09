using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record SavedOpportunityDto
    {
        [Required(ErrorMessage = " StudentId is  Required")]

        public string StudentId { get; set; }

        [Required(ErrorMessage = " PostId is  Required")]
        public int PostId { get; set; }
        [Required(ErrorMessage = " Type is  Required")]
        public char Type
        {
            get; set;
        }
    }
}
