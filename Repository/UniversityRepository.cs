using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UniversityRepository : RepositoryBase<University> , IUniversityRepository
    {
        public UniversityRepository( RepositoryContext context ) 
            : base( context ) { }  

        public void CreateUniversity( University university )  => Create(university);

    }
}
