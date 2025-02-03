using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;

namespace Service.Contracts
{
    public interface IScholarshipService
    {

        Task<IEnumerable<GetScholarshipDto>> GetAllScholarshipsForUniversity(string universityId, bool trackChanges);
        Task<GetScholarshipDto> CreateScholarshipForUniversity(string universityId, ScholarshipForCreationDto scholarshipForCreation, bool trackChanges);



        ScholarshipDto GetScholarship(string universityId, int id, bool trackChanges);
        Task UpdateScholarshipForUniversity(string universityId, int id, ScholarshipDto scholarshipForUpdateDto, 
        bool trackChanges);
        void DeleteScholarshipForUniversity(string universityId, int id, bool trackChanges);
    }
}
