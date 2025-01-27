using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ScholarshipReposiyory : RepositoryBase<Scholarship> , IScholarshipReposiyory
    {

        public ScholarshipReposiyory(RepositoryContext repositoryContext  ) :base(repositoryContext)  { }



        public void CreateScholarship(Scholarship scholarship) => Create(scholarship);



    }
}
