using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.DTO;

namespace Service.Contracts
{
    public interface IUniversityService
    {
        UniversityViewDto GetUniversity(string universityId, bool trackChanges);
        Task UpdateUniversity(string universityId, UniversityDto universityDto, bool trackChanges);
        Task<string> UploadProfilePictureAsync(IFormFile file, string universityId);

    }
}
