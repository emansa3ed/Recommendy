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
    }
}
