using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Entities.Models;
using System.Diagnostics.CodeAnalysis;

namespace Shared.DTO
{
	public class ScholarshipForCreationDto
	{

		[Required(ErrorMessage = "Name is required")]
		[StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Cost is required")]
		[Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive number")]
		public decimal Cost { get; set; }

		[Required(ErrorMessage = "Application deadline is required")]
		[DataType(DataType.Date)]
		public DateTime ApplicationDeadline { get; set; }

		[Required(ErrorMessage = "Start date is required")]
		[DataType(DataType.Date)]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "Duration is required")]
		[Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 day")]
		public int Duration { get; set; }

		[Required(ErrorMessage = "Degree is required")]
		[EnumDataType(typeof(Degree))]
		public Degree Degree { get; set; }

		[Required(ErrorMessage = "Funded status is required")]
		[EnumDataType(typeof(Funded))]
		public Funded Funded { get; set; }

		[StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Grants information is required")]
		public string Grants { get; set; }

		[Required(ErrorMessage = "URL for application form is required")]
		[Url(ErrorMessage = "Invalid URL format")]
		public string UrlApplicationForm { get; set; }

		[Required(ErrorMessage = "Eligible grade is required")]
		public string EligibleGrade { get; set; }
		public Dictionary<string, string> Requirements { get; set; } = new Dictionary<string, string>();
		[Required(ErrorMessage = "Image file is required")]
		public IFormFile? ImageFile { get; set; }
	}
}
