using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
	public interface ISkillService
	{
		Task<IEnumerable<string>> GetSkillsAsync(string userId);
		Task AddSkillsAsync(string userId, IEnumerable<string> skills);
		Task RemoveSkillAsync(string userId, string skill);
	}
}
