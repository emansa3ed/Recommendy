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

        Task<IEnumerable<EditedScholarshipDto>> GetAllScholarshipsForUniversity(string universityId, bool trackChanges);
        Task<EditedScholarshipDto> CreateScholarshipForUniversity(string universityId, ScholarshipForCreationDto scholarshipForCreation, bool trackChanges);



        Task<GetScholarshipDto> GetScholarshipById(int id, bool trackChanges);

        Task<IEnumerable<GetScholarshipDto>> GetAllScholarships(bool trackChanges);



		Task<ScholarshipDto> GetScholarshipAsync(string universityId, int id, bool trackChanges);
        Task UpdateScholarshipForUniversityAsync(string universityId, int id, ScholarshipDto scholarshipForUpdateDto, 
        bool trackChanges);
		Task DeleteScholarshipForUniversityAsync(string universityId, int id, bool trackChanges);


    }
}
