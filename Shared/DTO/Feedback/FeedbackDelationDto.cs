using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Feedback
{
    public record FeedbackDelationDto
    {
        [Required(ErrorMessage = "Feedback Id is required")]
        public int? Id { get; set; }


        [Required(ErrorMessage = "Feedback type is required")]
        [EnumDataType(typeof(FeedbackType), ErrorMessage = "Invalid feedback type")]
        public FeedbackType? Type { get; set; }
    }
}