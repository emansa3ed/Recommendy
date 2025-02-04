using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Contracts;

namespace Repository
{
    public class InternshipPositionRepository : RepositoryBase<InternshipPosition>, IInternshipPositionRepository
    {

        public InternshipPositionRepository(RepositoryContext context) : base(context) { }



        public void CreateInternshipPosition(InternshipPosition internshipPosition) => Create(internshipPosition);

        public void DeleteInternshipPosition(int InternshipId, int PositionId)
        {
            var result = FindByCondition(i => i.InternshipId == InternshipId && i.PositionId == PositionId, false);
          

            foreach (var item in result)
            {

                if (item != null)
                {
                    try
                    {
                        Delete(item);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Failed to Delete. {ex.Message} | Inner Exception: {ex.InnerException?.Message}");
                    }
                }
            }


        }

        public InternshipPosition GetInternshipPosition(int InternshipId, int PositionId)
        {

            var result = FindByCondition(i=>i.InternshipId == InternshipId && i.PositionId==PositionId, true).FirstOrDefault();

            return result;
        }
    }
}
