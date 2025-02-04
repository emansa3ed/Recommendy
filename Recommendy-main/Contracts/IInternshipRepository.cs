using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public  interface IInternshipRepository
    {

        void CreateIntership(Internship intership);
        void UpdateIntership(Internship intership);

        void DeleteIntership(Internship intership);

       Task<List<Internship>> GetInternshipsByCompanyId(string  id, bool trackChanges);


        IEnumerable<Internship> GetAllInternships(bool trackChanges);
        Internship GetInternshipById(int id, bool trackChanges);


    }
}
