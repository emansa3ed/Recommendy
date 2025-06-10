using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.RequestFeatures;
namespace Contracts
{
    public  interface IReportRepository
    {

        void CreateReport(Report report);
        void DeleteReport(Report report);
        Task<Report> GetReportAsync(int ReportId, bool trackChanges);
        Task<PagedList<Report>> GetReportsAsync( ReportParameters reportParameters, bool trackChanges);
        void UpdateReport(Report report);
    }
}
