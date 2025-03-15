using Entities.Models;
using Shared.DTO.Internship;
using Shared.DTO.opportunity;
using Shared.DTO.Scholaship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IOpportunityService
    {
        Task SavedOpportunity(string StudentId,SavedOpportunityDto savedPost);
        Task DeleteOpportunity(string StudentId,SavedOpportunityDto savedPost);

        Task<IEnumerable<GetScholarshipDto>> GetSavedScholarshipsAsync(string studentId);
        Task<IEnumerable<InternshipDto>> GetSavedInternshipsAsync(string studentId);

    }
}
