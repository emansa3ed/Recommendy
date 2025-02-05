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


    }
}
