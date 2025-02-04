using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;

namespace Service.Contracts
{
    public interface IInternshipPositionService
    {
        public Task<ApiResponse<int>> CreateInternshipPosition(InternshipPositionDto internshipPosition);
        public Task<bool> UpdateInternshipPosition(int InternshipId,int PositionId, InternshipPositionUpdateDto internshipPosition);
        public Task<ApiResponse<int>> DeleteInternshipPosition(int InternshipId  , int PositionId); 
            



    }
}
