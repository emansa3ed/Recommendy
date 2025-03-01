using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {

        public ReportRepository( RepositoryContext repositoryContext  )  : base(repositoryContext){ }


      public  void CreateReport( Report report ) =>Create(report);
      public void DeleteReport( Report report ) => Delete(report);

        public async Task<PagedList<Report>> GetReportsAsync(ReportParameters reportParameters, bool trackChanges)
        {
            var reports = await FindAll(trackChanges).OrderBy(e => e.CreatedAt).ToListAsync();
            var count = reports.Count();

            return new  PagedList<Report>(reports, count, reportParameters.PageNumber, reportParameters.PageSize);

           
        }
    }
}
