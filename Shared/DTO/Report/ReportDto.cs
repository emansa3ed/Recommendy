using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Shared.DTO.Report
{
    public record ReportDto
    {

        public int Id { get; set; }
        public int TypeId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public ReportType Type { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public object ReportedItem { get; set; }

    }
    public record UpdateReportStatusDto
    {

        [DefaultValue(true)]
        public bool? BanPost { get; set; } = true; 

        [DefaultValue(false)]
        public bool? DeletePost { get; set; } = false; 
    }
}
