using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Feedback
{
	public record FeedbackCreationDto
	{

        [Required(ErrorMessage = "StudentId type is required")]
        public string StudentId { get; set; }

        [Required(ErrorMessage = "Feedback type is required")]
		[EnumDataType(typeof(FeedbackType), ErrorMessage = "Invalid feedback type")]
		public FeedbackType Type { get; set; }

		[Required(ErrorMessage = "Content is required")]
		[StringLength(1000, ErrorMessage = "Content cannot be longer than 1000 characters")]
		public string Content { get; set; }

	}
}