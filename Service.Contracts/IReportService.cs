using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.DTO.Report;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IReportService
    {
         Task CreateReport(string StudentID, int PostID, ReportDtoCreation reportDtoCreation);

        Task DeleteReport(int ReportId);
        Task<ReportDto> GetReport(int ReportId);
        Task<PagedList<ReportDto>> GetReportsAsync(ReportParameters reportParameters, bool trackChanges);


    }
}
