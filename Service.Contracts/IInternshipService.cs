using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.GeneralResponse;
using Entities.Models;
using Shared.DTO.Internship;
using Shared.DTO.Report;
using Shared.RequestFeatures;


namespace Service.Contracts
{
    public interface IInternshipService
    {
       Task< Internship> CreateInternship( string CompanyId , InternshipCreationDto intersnhip);
        Task UpdateInternship(string CompanyId , int Id , InternshipUpdateDto internshipUpdateDto);

        Task DeleteInternship(string CompanyId,int Id , bool trackChanges);
        Task<PagedList<InternshipDto>> GetInternshipsByCompanyId(string companyId , InternshipParameters internshipParameters);


        
        Task<InternshipDto> GetInternshipById(int id, bool trackChanges);

        Task<PagedList<InternshipDto>> GetAllInternships(InternshipParameters internshipParameters, bool trackChanges);
        Task<PagedList<InternshipDto>> GetAllRecommendedInternships(string UserSkills,string Titles, InternshipParameters internshipParameters, bool trackChanges);


	}
}
