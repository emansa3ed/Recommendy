using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        void CreateCompany(Company company);
        Company GetCompany(string companyId, bool trackChanges);
        public void UpdateCompany(Company company);
    }

}
