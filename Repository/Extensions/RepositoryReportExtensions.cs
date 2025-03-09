using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryReportExtensions
    {
        public static IQueryable<Report> Paging(this IQueryable<Report> reports, int PageNumber, int PageSize)
        {
            return reports
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize);
        }
        public static IQueryable<Report> Filter(this IQueryable<Report> reports, string  Status) => (Status != null)
        ? reports
        .Where(e => e.Status.Equals(Status) )
        : reports;
    }

}
