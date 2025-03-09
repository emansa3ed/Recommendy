using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public  class ReportParameters : RequestParameters
    {
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }



    }
}
