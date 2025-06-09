
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Skills
{

	public record ResumeUploadDTO
	{
		[Required]
		public IFormFile ResumeFile { get; set; }

	}
}

