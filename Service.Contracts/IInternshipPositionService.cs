using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.GeneralResponse;
using Shared.DTO.Internship;

namespace Service.Contracts
{
    public interface IInternshipPositionService
    {
        public Task<InternshipPosition> CreateInternshipPosition( string CompanyId,int InternshipId, InternshipPositionDto internshipPosition);
        public Task UpdateInternshipPosition(string CompanyId, int InternshipId,int PositionId, InternshipPositionUpdateDto internshipPosition);
        public Task DeleteInternshipPosition(string CompanyId,int InternshipId  , int PositionId); 
            



    }
}
