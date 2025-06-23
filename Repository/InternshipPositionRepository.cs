using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class InternshipPositionRepository : RepositoryBase<InternshipPosition>, IInternshipPositionRepository
    {

        public InternshipPositionRepository(RepositoryContext context) : base(context) { }



        public void CreateInternshipPosition(InternshipPosition internshipPosition) => Create(internshipPosition);

        public async Task DeleteInternshipPosition(int InternshipId, int PositionId)
        {
            var result = await FindByCondition(i => i.InternshipId == InternshipId && i.PositionId == PositionId, true)
                                 .ToListAsync();

            if (result.Any())
            {
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
        }


        public InternshipPosition GetInternshipPosition(int InternshipId, int PositionId)
        {

            var result = FindByCondition(i=>i.InternshipId == InternshipId && i.PositionId==PositionId, true).FirstOrDefault();

            return result;
        }

        public async Task<List<InternshipPosition>> GetAllByInternshipIdAsync(int internshipId)
        {
            return await FindByCondition(i => i.InternshipId == internshipId, false).ToListAsync();
        }
    }
}
