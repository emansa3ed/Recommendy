using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public  class InternshipRepository : RepositoryBase<Internship> , IInternshipRepository 
    {
        public InternshipRepository(RepositoryContext repositoryContext) : base(repositoryContext) { 
        
       
        
        }


        public void CreateIntership(Internship internship)=> Create(internship);
        public void DeleteIntership(int Id, bool trackChanges)
        {
            var result = FindByCondition(i=>i.Id==Id , trackChanges);

            foreach (var item in result)
            {

                if (item != null)
                {
                    Delete(item);

                }
            }
        }

        public void UpdateIntership(  Internship internship) => Update(internship);

        public async Task<List<Internship>> GetInternshipsByCompanyId(string companyId, bool trackChanges)
        {
            return await FindByCondition(i => i.CompanyId == companyId, trackChanges)
                .Include(i => i.InternshipPositions)
                 .ThenInclude(ip => ip.Position)
                 .Include(i => i.Company) 
                 .ThenInclude(c => c.User) 
                 .ToListAsync();
        }



        public IEnumerable<Internship> GetAllInternships(bool trackChanges) =>
        FindAll(trackChanges)
        .Include(i => i.InternshipPositions)
        .ThenInclude(ip => ip.Position)
        .Include(i => i.Company) 
        .ThenInclude(c => c.User)
        .Where(i => i.IsBanned != true)
        .OrderByDescending(i => i.CreatedAt)
        .ToList();



        public Internship GetInternshipById(int id, bool trackChanges)
        {
            return FindByCondition(i => i.Id == id, trackChanges)
                .Where(i => i.IsBanned != true)
                .Include(i => i.InternshipPositions)
                    .ThenInclude(ip => ip.Position)
                .Include(i => i.Company)
                    .ThenInclude(c => c.User)
                .FirstOrDefault();
        }
    }
}
