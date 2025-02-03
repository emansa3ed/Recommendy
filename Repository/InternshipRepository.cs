using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Contracts;

namespace Repository
{
    public  class InternshipRepository : RepositoryBase<Internship> , IInternshipRepository 
    {
        public InternshipRepository(RepositoryContext repositoryContext) : base(repositoryContext) { 
        
       
        
        }



        public void CreateIntership(Internship internship)=> Create(internship);
        public void DeleteIntership(Internship internship) => Delete(internship);

        public void UpdateIntership(Internship internship) => Update(internship);

       


    }
}
