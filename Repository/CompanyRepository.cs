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
    CompanyParameters parameters,
    bool trackChanges)
        {
            var companies = FindAll(trackChanges)
                .Include(u => u.User)
                .ApplyUnverifiedFilter()
                .ApplyCompanySearch(parameters.SearchTerm)
                .AsNoTracking();

            return await companies.ToPagedListAsync(parameters);
        }

        public async Task<PagedList<Company>> GetAllCompaniesAsync(
            CompanyParameters parameters,
            bool trackChanges)
        {
            var companies = FindAll(trackChanges)
                .Include(u => u.User)
                .ApplyCompanySearch(parameters.SearchTerm)
                .AsNoTracking();

            return await companies.ToPagedListAsync(parameters);
        }


        public void DeleteCompany(Company company) => Delete(company);


    }
}
