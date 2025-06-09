using Entities.Models;
using Shared.RequestFeatures;
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
        Task<PagedList<University>> GetUnverifiedUniversitiesAsync(UniversityParameters universityParameters, bool trackChanges);
        void DeleteUniversity(University university);
    }
}
