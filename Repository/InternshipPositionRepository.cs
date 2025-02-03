using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Contracts;

namespace Repository
{
    public class InternshipPositionRepository : RepositoryBase<InternshipPosition> , IInternshipPositionRepository
    {

        public InternshipPositionRepository( RepositoryContext context ) : base( context ) { }
        


        public  void CreateInternshipPosition(InternshipPosition internshipPosition) => Create(internshipPosition);

       public  void UpdateInternshipPosition(InternshipPosition internshipPosition) => Update(internshipPosition);
        public void DeleteInternshipPosition(InternshipPosition internshipPosition) => Delete(internshipPosition);


    }
}
