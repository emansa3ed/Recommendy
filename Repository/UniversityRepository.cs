using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Shared.RequestFeatures.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Cloud.AIPlatform.V1.NearestNeighborQuery.Types;

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
        public async Task<PagedList<University>> GetUnverifiedUniversitiesAsync( UniversityParameters universityParameters, bool trackChanges)
        {
            var universities = FindAll(trackChanges)
                .Include(u => u.User)
                .Include(u => u.Country)
                .Where(u => !u.IsVerified)  
                .AsNoTracking();

            // Apply search if provided
            if (!string.IsNullOrWhiteSpace(universityParameters.SearchTerm))
            {
                universities = universities.Where(u =>
                    u.User.UserName.Contains(universityParameters.SearchTerm) ||
                    u.UniversityUrl.Contains(universityParameters.SearchTerm));
            }

            var count = await universities.CountAsync();
            var pagedUniversities = await universities
                .Skip((universityParameters.PageNumber - 1) * universityParameters.PageSize)
                .Take(universityParameters.PageSize)
                .ToListAsync();

            return new PagedList<University>(
                pagedUniversities,
                count,
                universityParameters.PageNumber,
                universityParameters.PageSize);
        }
   
            public async Task<PagedList<University>> GetAllUniversitiesAsync(
                UniversityParameters universityParameters,
                bool trackChanges)
            {
                var universities = FindAll(trackChanges)
                    .Include(u => u.User)
                    .Include(u => u.Country)
                    .AsNoTracking();

                // Apply search if provided
                if (!string.IsNullOrWhiteSpace(universityParameters.SearchTerm))
                {
                    universities = universities.Where(u =>
                        u.User.UserName.Contains(universityParameters.SearchTerm) ||
                        u.UniversityUrl.Contains(universityParameters.SearchTerm));
                }

                var count = await universities.CountAsync();
                var pagedUniversities = await universities
                    .Skip((universityParameters.PageNumber - 1) * universityParameters.PageSize)
                    .Take(universityParameters.PageSize)
                    .ToListAsync();

                return new PagedList<University>(
                    pagedUniversities,
                    count,
                    universityParameters.PageNumber,
                    universityParameters.PageSize);
            }
        
        public void DeleteUniversity(University university) => Delete(university);
    }
}
