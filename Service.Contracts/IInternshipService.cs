using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.DTO;


namespace Service.Contracts
{
    public interface IInternshipService
    {
       Task< ApiResponse<Internship>> CreateInternship(InternshipCreationDto intersnhip);
        Task UpdateInternship(Internship intersnhip);

        Task DeleteInternship(Internship intersnhip);
        Task<List<InternshipDto>> GetInternshipsByCompanyId(string companyId);


    }
}
