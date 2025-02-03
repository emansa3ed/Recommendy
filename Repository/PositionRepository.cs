using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;

namespace Repository
{
    public class PositionRepository : RepositoryBase<Position> , IPositionRepository
    {
        public PositionRepository(RepositoryContext repositoryContext) : base(repositoryContext){

        }


        public IQueryable<Position> GetAllPositions(bool trackChanges)
        {
            return FindAll(trackChanges);

        }

    }
}
