using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Report
{
    public record ReportDtoCreation
    {

        [Required(ErrorMessage = "Type is required")]
        [EnumDataType(typeof(ReportType), ErrorMessage = "Invalid feedback type")]
        public ReportType Type { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [StringLength(1000, ErrorMessage = "Content cannot be longer than 1000 characters")]
        public string Content { get; set; }
    }
}
