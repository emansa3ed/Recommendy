using Entities.Models;
using Microsoft.AspNetCore.Mvc;
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
        Task SavedOpportunity(string StudentId, int PostId,  char Type);
        Task DeleteOpportunity(string StudentId, int PostId, char Type);

        Task<IEnumerable<GetScholarshipDto>> GetSavedScholarshipsAsync(string studentId);
        Task<IEnumerable<InternshipDto>> GetSavedInternshipsAsync(string studentId);

    }
}
