using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;

namespace Service.Contracts
{

    public interface ICompanyService
    {
        CompanyViewDto GetCompany(string companyId, bool trackChanges);
        Task UpdateCompany(string companyId, CompanyDto companyDto, bool trackChanges);
       
    }
}
