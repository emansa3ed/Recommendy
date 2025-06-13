using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using Shared.RequestFeatures.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UniversityRepository : RepositoryBase<University>, IUniversityRepository
    {
        public UniversityRepository(RepositoryContext context)
            : base(context)
        {
        }

        public void CreateUniversity(University university) => Create(university);
        public async Task<University> GetUniversityAsync(string universityId, bool trackChanges) =>
          await FindByCondition(u => u.UniversityId.Equals(universityId), trackChanges)
         .Include(u => u.User)
         .Include(u => u.Country)
         .SingleOrDefaultAsync();

        public void UpdateUniversity(University university) => Update(university);
        public async Task<PagedList<University>> GetUnverifiedUniversitiesAsync(
     UniversityParameters parameters,
     bool trackChanges)
        {
            var universities = FindAll(trackChanges)
                .Include(u => u.User)
                .Include(u => u.Country)
                .ApplyUnverifiedFilter()
                .ApplyUniversitySearch(parameters.SearchTerm)
                .AsNoTracking();

            return await universities.ToPagedListAsync(parameters);
        }

        public async Task<PagedList<University>> GetAllUniversitiesAsync(
            UniversityParameters parameters,
            bool trackChanges)
        {
            var universities = FindAll(trackChanges)
                .Include(u => u.User)
                .Include(u => u.Country)
                .ApplyUniversitySearch(parameters.SearchTerm)
                .AsNoTracking();

            return await universities.ToPagedListAsync(parameters);
        }



        public void DeleteUniversity(University university) => Delete(university);
    }
}
