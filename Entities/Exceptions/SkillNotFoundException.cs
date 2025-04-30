using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class SkillNotFoundException : NotFoundException
	{
		public SkillNotFoundException(string skill)
		  : base($"The Skill {skill} doesn't exist in the database.")
		{
		}
	}
}
