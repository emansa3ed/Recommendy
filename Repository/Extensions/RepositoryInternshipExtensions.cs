using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryInternshipExtensions
    {
        public static IQueryable<Internship> Filter(this IQueryable<Internship> internships, bool? paid)
        {
            if (paid.HasValue)
                return internships.Where(i => i.Paid == paid.Value);

            return internships; 
        }

        public static IQueryable<Internship> Paging(this IQueryable<Internship> internships, int PageNumber, int PageSize)
        {
            return internships
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize);
        }
		public static IQueryable<Internship> Search(this IQueryable<Internship> internships, string searchTerms)
		{
			if (string.IsNullOrWhiteSpace(searchTerms))
				return internships;

			var terms = searchTerms
				.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(term => term.ToLower())
				.ToList();

			return internships.Where(internship =>
				terms.Any(term => internship.Name.ToLower().Contains(term)));
		}


		public static IQueryable<Internship> Recommendation(this IQueryable<Internship> internships, string jobTitles)
		{
			if (string.IsNullOrWhiteSpace(jobTitles))
				return internships;
			var terms = jobTitles
				.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(term => int.Parse(term))
				.ToList();

			return internships.Where(Scholarship => terms.Contains(Scholarship.Id));
		}

	}

}
