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
    public class ScholarshipRepository : RepositoryBase<Scholarship>, IScholarshipRepository
    {

        public ScholarshipRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Scholarship>> GetAllScholarshipsAsync(string universityId, bool trackChanges) =>
          await FindByCondition(s => s.UniversityId.Equals(universityId), trackChanges)
                .OrderBy(s => s.Name).ToListAsync();


        public void CreateScholarship(Scholarship scholarship) => Create(scholarship);

        public Scholarship GetScholarship(string universityId, int id, bool trackChanges) =>
            FindByCondition(s => s.UniversityId.Equals(universityId) && s.Id.Equals(id), trackChanges).SingleOrDefault();
        public void UpdateScholarship(Scholarship scholarship) => Update(scholarship);
        public void DeleteScholarship(Scholarship scholarship) => Delete(scholarship);

        public IEnumerable<Scholarship> GetAllScholarships(bool trackChanges) => FindAll(trackChanges).Where(s => !s.IsBanned).OrderByDescending(s => s.CreatedAt).ToList();
        public Scholarship GetScholarshipById(int id, bool trackChanges) => FindByCondition(s => s.Id == id, trackChanges).Where(s => !s.IsBanned).SingleOrDefault();



    }
}
