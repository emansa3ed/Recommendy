using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResumeModels
{
	public class Resume
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public List<string> Skills { get; set; } = new List<string>();
		public List<WorkExperience> WorkExperience { get; set; } = new List<WorkExperience>();
		public List<Education> Education { get; set; } = new List<Education>();
		public string Summary { get; set; }
	}
}
