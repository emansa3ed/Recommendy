using Contracts;
using DocumentFormat.OpenXml.Spreadsheet;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SkillService :ISkillService
    {
		private readonly IRepositoryManager _repository;

		public SkillService(IRepositoryManager repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<string>> GetSkillsAsync(string userId)
		{
			var student =  _repository.Student.GetStudent(userId, trackChanges: false);
			if (student == null)
				throw new UserNotFoundException(userId);

			return string.IsNullOrWhiteSpace(student.Skills)
		   ? Enumerable.Empty<string>()
		   : student.Skills.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		}

		public async Task AddSkillsAsync(string userId, IEnumerable<string> skills)
		{
			var student = _repository.Student.GetStudent(userId, trackChanges: true);
			if (student == null)
				throw new UserNotFoundException(userId);

			var existingSkills = string.IsNullOrWhiteSpace(student.Skills)
			? new List<string>()
			: student.Skills.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();

			foreach (var skill in skills.Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)))
			{
				if (!existingSkills.Contains(skill, StringComparer.OrdinalIgnoreCase))
				{
					existingSkills.Add(skill);
				}
			}

			student.Skills = string.Join(",", existingSkills);
			await _repository.SaveAsync();
		}

		public async Task RemoveSkillAsync(string userId, string skill)
		{
			var student = _repository.Student.GetStudent(userId, trackChanges: true);
			if (student == null)
				throw new UserNotFoundException(userId);

			var existingSkills = string.IsNullOrWhiteSpace(student.Skills)
				? new List<string>()
				: student.Skills.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
			var caount = existingSkills.Count();

			existingSkills = existingSkills
				.Where(s => !s.Equals(skill, StringComparison.OrdinalIgnoreCase))
				.ToList();
			if (existingSkills.Count() == caount)
				throw new SkillNotFoundException(skill);

			student.Skills = string.Join(",", existingSkills);
			await _repository.SaveAsync();
		}
	}
}
