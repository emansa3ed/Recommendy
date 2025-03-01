using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Report
{
    public record ReportDto
    {


        public ReportType Type { get; set; }

        public string Content
        {
            get; set;
        }
    }
}
