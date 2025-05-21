using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
  
        public interface IScholarshipRepository
        {
            Task<PagedList<Scholarship>> GetAllScholarshipsAsync(string universityId, ScholarshipsParameters scholarshipsParameters, bool trackChanges);
            Scholarship GetScholarship(string universityId, int id, bool trackChanges);
            void CreateScholarship(Scholarship scholarship);
            void UpdateScholarship(Scholarship scholarship);
            void DeleteScholarship(Scholarship scholarship);
            Task<PagedList<Scholarship>> GetAllScholarshipsAsync(ScholarshipsParameters scholarshipsParameters, bool trackChanges);
            Task<PagedList<Scholarship>> GetAllRecommendedScholarships(string UserSkills,ScholarshipsParameters scholarshipsParameters, bool trackChanges);
            Scholarship GetScholarshipById(int id, bool trackChanges);
    }

    
}
