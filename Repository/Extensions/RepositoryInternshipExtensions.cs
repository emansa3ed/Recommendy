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
        public static IQueryable<Internship> Search(this IQueryable<Internship> Internship, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Internship;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return Internship.Where(e => (e.Name.ToLower().Contains(lowerCaseTerm)));
        }
    }

}
