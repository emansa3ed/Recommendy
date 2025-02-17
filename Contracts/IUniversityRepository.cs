using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUniversityRepository
    {
        Task<University> GetUniversityAsync(string universityId, bool trackChanges);
        void CreateUniversity(University university);
        public void UpdateUniversity(University university);
    }
}
