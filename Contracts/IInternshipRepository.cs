using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{ 
    public  interface IInternshipRepository
    {

        void CreateIntership(Internship intership);
        void UpdateIntership(Internship intership);

        void DeleteIntership(int Id, bool trackChanges);

        Task<PagedList<Internship>> GetInternshipsByCompanyId(string  id, InternshipParameters internshipParameters, bool trackChanges);


        Task<PagedList<Internship>> GetAllInternshipsAsync(InternshipParameters internshipParameters,bool trackChanges);
        Task<PagedList<Internship>> GetAllRecommendedInternships(string UserSkills,InternshipParameters internshipParameters,bool trackChanges);
        Internship GetInternshipById(int id, bool trackChanges);
        Task DeleteInternshipsByCompanyId(string companyId);


    }
}
