using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {

        ICountryRepository Country { get; }
        IStudentRepository Student { get; }
        IUserRepository User { get; }
        ICompanyRepository Company { get; }
        IUniversityRepository university { get; }
        IScholarshipReposiyory scholarship { get; } 

        IFileRepository File { get; }

        IInternshipRepository Intership { get; }

        IInternshipPositionRepository InternshipPosition { get; }   




        Task SaveAsync();
    }
}
