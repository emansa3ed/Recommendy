using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ScholarshipRepository : RepositoryBase<Scholarship>, IScholarshipRepository
    {

        public ScholarshipRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Scholarship>> GetAllScholarshipsAsync(string universityId, ScholarshipsParameters scholarshipsParameters, bool trackChanges)
        {
			var res = await FindByCondition(s => s.UniversityId.Equals(universityId), trackChanges)
			    .Paging(scholarshipsParameters.PageNumber, scholarshipsParameters.PageSize)
			    .Filter(scholarshipsParameters.fund, scholarshipsParameters.degree)
                .Search(scholarshipsParameters.searchTerm)
				.ToListAsync();
			var count = await FindByCondition(s => s.UniversityId.Equals(universityId), trackChanges).CountAsync();

			return new PagedList<Scholarship>(res, count, scholarshipsParameters.PageNumber, scholarshipsParameters.PageSize);
		}


		public void CreateScholarship(Scholarship scholarship) => Create(scholarship);


        public Scholarship GetScholarship(string universityId, int id, bool trackChanges) =>
            FindByCondition(s => s.UniversityId.Equals(universityId) && s.Id.Equals(id), trackChanges).SingleOrDefault();
        public void UpdateScholarship(Scholarship scholarship) => Update(scholarship);
        public void DeleteScholarship(Scholarship scholarship) => Delete(scholarship);

		//  public IEnumerable<Scholarship> GetAllScholarships(bool trackChanges) => FindAll(trackChanges).Where(s => !s.IsBanned).OrderByDescending(s => s.CreatedAt).ToList();


		public async Task<PagedList<Scholarship>> GetAllScholarshipsAsync(ScholarshipsParameters scholarshipsParameters, bool trackChanges)
        {
			var res = await FindByCondition((s => !s.IsBanned),trackChanges)
			    .Paging(scholarshipsParameters.PageNumber, scholarshipsParameters.PageSize)
				.Filter(scholarshipsParameters.fund, scholarshipsParameters.degree)
				.Search(scholarshipsParameters.searchTerm)
				.Include(s => s.University)
				.ThenInclude(u => u.User)
				.ToListAsync();

			var count = await FindByCondition((s => !s.IsBanned), trackChanges).Search(scholarshipsParameters.searchTerm).CountAsync();
			return new PagedList<Scholarship>(res, count, scholarshipsParameters.PageNumber, scholarshipsParameters.PageSize);
		}

        public Scholarship GetScholarshipById(int id, bool trackChanges) => FindByCondition(s => s.Id == id, trackChanges).Where(s => !s.IsBanned).Include(s => s.University)
                .ThenInclude(u => u.User).SingleOrDefault();

        public Scholarship ScholarshipById(int id, bool trackChanges) => FindByCondition(s => s.Id == id, trackChanges).Include(s => s.University)
              .ThenInclude(u => u.User).SingleOrDefault();

        public async Task<PagedList<Scholarship>> GetAllRecommendedScholarships(string UserSkills, ScholarshipsParameters scholarshipsParameters, bool trackChanges)
		{
			var res = await FindByCondition((s => !s.IsBanned), trackChanges)
				.Paging(scholarshipsParameters.PageNumber, scholarshipsParameters.PageSize)
				.Filter(scholarshipsParameters.fund, scholarshipsParameters.degree)
				.Recommendation(UserSkills)
				.Include(s => s.University)
				.ThenInclude(u => u.User)
				.ToListAsync();

			var count = await FindByCondition((s => !s.IsBanned), trackChanges).Recommendation(UserSkills).CountAsync();
			return new PagedList<Scholarship>(res, count, scholarshipsParameters.PageNumber, scholarshipsParameters.PageSize);
		}

        public async Task DeleteScholarshipsByUniversityId(string universityId)
        {
            var scholarships = await FindByCondition(s => s.UniversityId == universityId, false).ToListAsync();
            foreach (var scholarship in scholarships)
            {
                Delete(scholarship);
            }
        }
    }
}
