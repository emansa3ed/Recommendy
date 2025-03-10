using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Repository.Extensions;

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

        public async Task<PagedList<Internship>> GetInternshipsByCompanyId(string companyId,InternshipParameters internshipParameters ,bool trackChanges)
        {
            var internship =  await FindByCondition(i => i.CompanyId == companyId, trackChanges)
                .Include(i => i.InternshipPositions)
                 .ThenInclude(ip => ip.Position)
                 .Include(i => i.Company)
                 .ThenInclude(c => c.User)
                 .Paging(internshipParameters.PageNumber, internshipParameters.PageSize)
                 .ToListAsync();

            var count = await FindByCondition(i => i.CompanyId == companyId, trackChanges)
                .Include(i => i.InternshipPositions)
                 .ThenInclude(ip => ip.Position)
                 .Include(i => i.Company)
                 .ThenInclude(c => c.User)
                 .CountAsync();


            return new PagedList<Internship>(internship, count, internshipParameters.PageNumber, internshipParameters.PageSize);

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
