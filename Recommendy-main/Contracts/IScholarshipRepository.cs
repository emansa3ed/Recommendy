using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
  
        public interface IScholarshipRepository
        {
            Task<IEnumerable<Scholarship>> GetAllScholarshipsAsync(string universityId, bool trackChanges);
            Scholarship GetScholarship(string universityId, int id, bool trackChanges);
            void CreateScholarship(Scholarship scholarship);
            void UpdateScholarship(Scholarship scholarship);
            void DeleteScholarship(Scholarship scholarship);
            IEnumerable<Scholarship> GetAllScholarships(bool trackChanges);
            Scholarship GetScholarshipById(int id, bool trackChanges);
    }

    
}
