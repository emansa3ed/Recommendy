using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.GeneralResponse;
using Entities.Models;
using Shared.DTO.Internship;


namespace Service.Contracts
{
    public interface IInternshipService
    {
       Task< Internship> CreateInternship( string CompanyId , InternshipCreationDto intersnhip);
        Task UpdateInternship(string CompanyId , int Id , InternshipUpdateDto internshipUpdateDto);

        Task DeleteInternship(string CompanyId,int Id , bool trackChanges);
        Task<List<InternshipDto>> GetInternshipsByCompanyId(string companyId);


        
        Task<InternshipDto> GetInternshipById(int id, bool trackChanges);

        Task<IEnumerable<InternshipDto>> GetAllInternships(bool trackChanges);


    }
}
