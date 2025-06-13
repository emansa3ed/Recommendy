using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
	public static class RepositoryScholarshipExtensions
	{
		public static IQueryable<Scholarship> Paging(this IQueryable<Scholarship> Scholarship, int PageNumber, int PageSize)
		{
			return Scholarship
			.Skip((PageNumber - 1) * PageSize)
			.Take(PageSize);
		}
		public static IQueryable<Scholarship> Filter(this IQueryable<Scholarship> Scholarship, Funded? funded, Degree? degree) => (funded != null && degree != null)
			? Scholarship
			.Where(e => (e.Funded.Equals(funded) && e.Degree.Equals(degree)))
			: Scholarship;
		public static IQueryable<Scholarship> Search(this IQueryable<Scholarship> Scholarship, string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
				return Scholarship;
			var lowerCaseTerm = searchTerm.Trim().ToLower();
			return Scholarship.Where(e => (e.Name.ToLower().Contains(lowerCaseTerm)));
		}
		public static IQueryable<Scholarship> Recommendation(this IQueryable<Scholarship> Scholarship, string jobTitles)
		{
			if (string.IsNullOrWhiteSpace(jobTitles))
				return Scholarship;
			var terms = jobTitles
				.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(term => term.ToLower())
				.ToList();

			return Scholarship.Where(Scholarship =>
				terms.Any(term => Scholarship.Name.ToLower().Contains(term) || Scholarship.Description.ToLower().Contains(term)));
		}
	}
}
