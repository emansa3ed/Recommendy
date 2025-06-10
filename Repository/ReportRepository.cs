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
using Shared.DTO.Report;
using Repository.Extensions;

namespace Repository
{
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {

        public ReportRepository( RepositoryContext repositoryContext  )  : base(repositoryContext){ }


        public  void CreateReport( Report report ) =>Create(report);
        public void DeleteReport( Report report ) => Delete(report);

        public async Task<Report> GetReportAsync(int ReportId, bool trackChanges)
        {
           var report= await FindByCondition(a => a.Id == ReportId, trackChanges).SingleOrDefaultAsync();

            return  report;  

        }

        public async Task<PagedList<Report>> GetReportsAsync(ReportParameters reportParameters, bool trackChanges)
        {
            var reports = await FindAll(trackChanges).Filter(reportParameters.Status).Paging(reportParameters.PageNumber,reportParameters.PageSize).OrderBy(e => e.CreatedAt).ToListAsync();
            var count = await FindAll(trackChanges).CountAsync();

            return new  PagedList<Report>(reports, count, reportParameters.PageNumber, reportParameters.PageSize);

           
        }


        public void UpdateReport(Report report) => Update(report);
    }
}
