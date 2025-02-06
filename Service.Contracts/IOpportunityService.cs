using Entities.Models;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IOpportunityService
    {
        Task<string> SavedOpportunity(SavedOpportunityDto savedPost);
        Task<string> DeleteOpportunity(SavedOpportunityDto savedPost);

        Task<IEnumerable<GetScholarshipDto>> GetSavedScholarshipsAsync(string studentId);
        Task<IEnumerable<InternshipDto>> GetSavedInternshipsAsync(string studentId);

    }
}
