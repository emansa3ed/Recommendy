using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class FeedBackParameters : RequestParameters
    {
        [Required(ErrorMessage = "Type is required")]
        public FeedbackType? type { get; set; }
    }
}