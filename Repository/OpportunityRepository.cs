using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public  class OpportunityRepository : RepositoryBase<SavedPost>, IOpportunityRepository
    {
        public OpportunityRepository( RepositoryContext repositoryContext) : base(repositoryContext) 
        {
        }


        public async  Task<string> SavedOpportunity(SavedPost savedPost)
        {
            try
            {
                Create(savedPost);
                return "created";
            }
            catch(Exception ex) 
            {
               return($"Failed Create ");
            }

        }

        public  async  Task<string> DeleteOpportunity(SavedPost savedPost)
        {
            try
            {
                Delete(savedPost);
                return "Deleted";
            }
            catch (Exception ) 
            {
                return($"Failed Delete");


            }

        }
        public async Task<SavedPost> GetSavedOpportunity(string StudentId, int PostId, char Type)
        {
             var savedpost =   FindByCondition(i => i.StudentId == StudentId && i.PostId == PostId && i.Type == Type, false).FirstOrDefault();

            return savedpost;


        }

        public async Task<IEnumerable<SavedPost>> GetSavedScholarshipsAsync(string studentId, bool trackChanges)
        {
            return await RepositoryContext.SavedPosts
                .Where(sp => sp.StudentId == studentId && sp.Type == 'S')
                .ToListAsync();
        }

        public async Task<IEnumerable<SavedPost>> GetSavedInternshipsAsync(string studentId, bool trackChanges)
        {
            return await RepositoryContext.SavedPosts
                .Where(sp => sp.StudentId == studentId && sp.Type == 'I')
                .ToListAsync();
        }



    }
}
