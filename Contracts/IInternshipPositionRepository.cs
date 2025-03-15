using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IInternshipPositionRepository
    {

        void CreateInternshipPosition(InternshipPosition internshipPosition);

        Task DeleteInternshipPosition(int InternshipId ,int PositionId);
        InternshipPosition GetInternshipPosition(int InternshipId, int PositionId);
    }
}
