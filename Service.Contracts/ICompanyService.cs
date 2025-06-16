using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO.Company;
using Shared.RequestFeatures;

namespace Service.Contracts
{

    public interface ICompanyService
    {
        CompanyViewDto GetCompany(string companyId, bool trackChanges);
        Task UpdateCompany(string companyId, CompanyDto companyDto, bool trackChanges);
        Task<PagedList<CompanyViewDto>> GetUnverifiedCompaniesAsync(CompanyParameters companyParameters, bool trackChanges);
        Task VerifyCompany(string companyId, string adminId, CompanyVerificationDto verificationDto, bool trackChanges);
    }
}
