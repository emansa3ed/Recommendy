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
        public static IQueryable<Internship> Filter(this IQueryable<Internship> internships, bool paid)
	    {
            return internships
		    .Where(i => i.Paid == paid);
        }
        public static IQueryable<Internship> Paging(this IQueryable<Internship> internships, int PageNumber, int PageSize)
        {
            return internships
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize);
        }

	}
}
