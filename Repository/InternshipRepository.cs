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
	public class InternshipRepository : RepositoryBase<Internship>, IInternshipRepository
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
                 .Search(internshipParameters.searchTerm)
				 .ToListAsync();

            var count = await FindByCondition(i => i.CompanyId == companyId, trackChanges)
				 .Search(internshipParameters.searchTerm)
				.Include(i => i.InternshipPositions)
                 .ThenInclude(ip => ip.Position)
                 .Include(i => i.Company)
                 .ThenInclude(c => c.User)
                 .CountAsync();


            return new PagedList<Internship>(internship, count, internshipParameters.PageNumber, internshipParameters.PageSize);

        }




		public async Task<PagedList<Internship>> GetAllInternshipsAsync(InternshipParameters internshipParameters,bool trackChanges)
        {
		    var res = await FindByCondition((i => i.IsBanned != true && i.ApplicationDeadline>=DateTime.Now), trackChanges).Include(i => i.InternshipPositions)
                    .ThenInclude(ip => ip.Position)
                .Filter(internshipParameters.Paid).Paging(internshipParameters.PageNumber, internshipParameters.PageSize).Search(internshipParameters.searchTerm)
                .Include(i => i.Company)
                .ThenInclude(c => c.User)
                .ToListAsync();

            var count = await FindByCondition((i => i.IsBanned != true && i.ApplicationDeadline >= DateTime.Now), trackChanges).Search(internshipParameters.searchTerm).CountAsync();
			return new PagedList<Internship>(res, count, internshipParameters.PageNumber, internshipParameters.PageSize);

		}



		public Internship GetInternshipById(string CompanyID, int id, bool trackChanges)
        {
            return FindByCondition(i => i.Id == id && i.CompanyId.Equals(CompanyID), trackChanges)
                .Where(i => i.IsBanned != true)
                .Include(i => i.InternshipPositions)
                    .ThenInclude(ip => ip.Position)
                .Include(i => i.Company)
                    .ThenInclude(c => c.User)
                .FirstOrDefault();
        }

        public Internship InternshipById(int id, bool trackChanges)
        {
            return FindByCondition(i => i.Id == id, trackChanges)
                .Include(i => i.InternshipPositions)
                    .ThenInclude(ip => ip.Position)
                .Include(i => i.Company)
                    .ThenInclude(c => c.User)
                .FirstOrDefault();
        }


        public async Task<PagedList<Internship>> GetAllRecommendedInternships(string Titles, InternshipParameters internshipParameters, bool trackChanges)
		{
			var res = await FindByCondition((i => i.IsBanned != true && i.ApplicationDeadline >= DateTime.Now), trackChanges).Include(i => i.InternshipPositions)
			.ThenInclude(ip => ip.Position)
		.Filter(internshipParameters.Paid).Paging(internshipParameters.PageNumber, internshipParameters.PageSize).Recommendation(Titles)
		.Include(i => i.Company)
		.ThenInclude(c => c.User)
		.ToListAsync();

			var count = await FindByCondition((i => i.IsBanned != true && i.ApplicationDeadline >= DateTime.Now), trackChanges)
		.Filter(internshipParameters.Paid)
        .Paging(internshipParameters.PageNumber, internshipParameters.PageSize)
        .Recommendation(Titles)
		.CountAsync();
			return new PagedList<Internship>(res, count, internshipParameters.PageNumber, internshipParameters.PageSize);
		}

        public async Task DeleteInternshipsByCompanyId(string companyId)
        {
            var internships = await FindByCondition(i => i.CompanyId == companyId, false).ToListAsync();
            foreach (var internship in internships)
            {
                Delete(internship);
            }
        }
    }
}
