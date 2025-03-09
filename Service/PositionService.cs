using Contracts;
using Entities.Models;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Contracts;
using Entities.Exceptions;

namespace Service
{
    public class PositionService: IPositionService
    {

        private readonly IRepositoryManager _repositoryManager;

        public PositionService(IRepositoryManager repositoryManager)
        {
            this._repositoryManager = repositoryManager;
        }

       public IQueryable<Position> GetAllPositions( string CompanyId,bool trackChanges)
        {

            var company = _repositoryManager.Company.GetCompany(CompanyId, false);
            if (company == null)
                throw new CompanyNotFoundException(CompanyId);

            var result=   _repositoryManager.PositionRepository.GetAllPositions(trackChanges);
            return result;
        }
    }
}
