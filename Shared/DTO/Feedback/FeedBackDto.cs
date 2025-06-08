using Shared.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Feedback
{
	public record FeedBackDto
	{
        public int Id { get; set; }
        public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
        public StudentDto Student { get; set; }
    }
}
