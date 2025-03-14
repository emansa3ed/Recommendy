using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO.Scholaship;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IScholarshipService
    { 

        Task<PagedList<EditedScholarshipDto>> GetAllScholarshipsForUniversity(string universityId, ScholarshipsParameters scholarshipsParameters, bool trackChanges);
        Task<EditedScholarshipDto> CreateScholarshipForUniversity(string universityId, ScholarshipForCreationDto scholarshipForCreation, bool trackChanges);



        Task<GetScholarshipDto> GetScholarshipById(int id, bool trackChanges);

        Task<PagedList<GetScholarshipDto>> GetAllScholarships(ScholarshipsParameters scholarshipsParameters,bool trackChanges);



		Task<ScholarshipDto> GetScholarshipAsync(string universityId, int id, bool trackChanges);
        Task UpdateScholarshipForUniversityAsync(string universityId, int id, ScholarshipDto scholarshipForUpdateDto, 
        bool trackChanges);
		Task DeleteScholarshipForUniversityAsync(string universityId, int id, bool trackChanges);


    }
}
