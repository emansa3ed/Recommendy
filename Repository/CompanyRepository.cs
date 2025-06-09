using Contracts;
using Entities.Models;
using Google.Cloud.AIPlatform.V1;
using Humanizer;
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
    public class CompanyRepository : RepositoryBase<Company>,ICompanyRepository
    {

        public CompanyRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext) { }

        public void CreateCompany(Company company)=> Create(company);
        public Company GetCompany(string companyId, bool trackChanges) =>
        FindByCondition(c => c.CompanyId.Equals(companyId), trackChanges)
       .Include(u => u.User)
       .SingleOrDefault();

        public void UpdateCompany(Company company) => Update(company);
        public async Task<PagedList<Company>> GetUnverifiedCompaniesAsync(
    CompanyParameters companyParameters,
    bool trackChanges)
        {
            var companies = FindAll(trackChanges)
                .Include(u => u.User)               
                .Where(u => !u.IsVerified) 
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(companyParameters.SearchTerm))
            {
                companies = companies.Where(u =>
                    u.User.UserName.Contains(companyParameters.SearchTerm) ||
                    u.CompanyUrl.Contains(companyParameters.SearchTerm));
            }

            var count = await companies.CountAsync();
            var pagedCompanies = await companies
                .Skip((companyParameters.PageNumber - 1) * companyParameters.PageSize)
                .Take(companyParameters.PageSize)
                .ToListAsync();

            return new PagedList<Company>(
                pagedCompanies,
                count,
                companyParameters.PageNumber,
                companyParameters.PageSize);
        }


        public void DeleteCompany(Company company) => Delete(company);


    }
}
