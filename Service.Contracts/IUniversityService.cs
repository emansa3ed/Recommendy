using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.DTO.Company;
using Shared.DTO.University;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IUniversityService
    {
        Task<UniversityViewDto> GetUniversityAsync(string universityId, bool trackChanges);
        Task UpdateUniversity(string universityId, UniversityDto universityDto, bool trackChanges);
        Task<PagedList<UniversityViewDto>> GetUnverifiedUniversitiesAsync(UniversityParameters universityParameters, bool trackChanges);
        Task VerifyUniversity(string universityId, string adminId, UniversityVerificationDto verificationDto, bool trackChanges);

    }
}
