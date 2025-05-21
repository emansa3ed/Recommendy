using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IOpportunityRepository
    {
        Task<string> SavedOpportunity(SavedPost savedPost);
        Task<string> DeleteOpportunity(SavedPost savedPost);

        Task<SavedPost> GetSavedOpportunity(string StudentId, int PostId, char Type);
		Task<IEnumerable<SavedPost>> GetSavedScholarshipsAsync(string studentId, bool trackChanges);
        Task<IEnumerable<SavedPost>> GetSavedInternshipsAsync(string studentId, bool trackChanges);


    }
}
