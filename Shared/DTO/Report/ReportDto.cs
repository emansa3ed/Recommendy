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

        public int Id { get; set; }
        public int TypeId { get; set; }


        public ReportType Type { get; set; }

        public string Content
        {
            get; set;
        }
    }
}
