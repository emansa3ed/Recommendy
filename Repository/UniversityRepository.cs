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
    public class UniversityRepository : RepositoryBase<University>, IUniversityRepository
    {
        public UniversityRepository(RepositoryContext context)
            : base(context)
        {
        }

        public void CreateUniversity(University university) => Create(university);
        public University GetUniversity(string universityId, bool trackChanges) =>
          FindByCondition(u => u.UniversityId.Equals(universityId), trackChanges)
         .Include(u => u.User)
         .SingleOrDefault();

        public void UpdateUniversity(University university) => Update(university);
    }
}
