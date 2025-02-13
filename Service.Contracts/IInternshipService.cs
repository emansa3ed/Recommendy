using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.GeneralResponse;
using Entities.Models;
using Shared.DTO;


namespace Service.Contracts
{
    public interface IInternshipService
    {
       Task< ApiResponse<Internship>> CreateInternship(InternshipCreationDto intersnhip);
        Task<bool> UpdateInternship( int Id , InternshipUpdateDto internshipUpdateDto);

        Task<ApiResponse<int>> DeleteInternship(int Id , bool trackChanges);
        Task<List<InternshipDto>> GetInternshipsByCompanyId(string companyId);


        
        Task<InternshipDto> GetInternshipById(int id, bool trackChanges);

        Task<IEnumerable<InternshipDto>> GetAllInternships(bool trackChanges);


    }
}
