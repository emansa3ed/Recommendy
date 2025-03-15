using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.opportunity
{
    public record SavedOpportunityDto
    {

        [Required(ErrorMessage = " PostId is  Required")]
        public int PostId { get; set; }
        [Required(ErrorMessage = " Type is  Required")]
        public char Type
        {
            get; set;
        }
    }
}
