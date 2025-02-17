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
        Task<UniversityViewDto> GetUniversityAsync(string universityId, bool trackChanges);
        Task UpdateUniversity(string universityId, UniversityDto universityDto, bool trackChanges);


    }
}
