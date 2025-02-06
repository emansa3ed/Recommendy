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

    }
}
