using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.GeneralResponse;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO.Internship;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service
{
    public class InternshipPositionService : IInternshipPositionService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public InternshipPositionService(IRepositoryManager repositoryManager, IMapper mapper)
        {

            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }


        public async Task<InternshipPosition> CreateInternshipPosition( string CompanyId, int InternshipId, InternshipPositionDto internshipPositionDto)
        {
            var company = _repositoryManager.Company.GetCompany(CompanyId, false);
            if (company == null)
                throw new CompanyNotFoundException(CompanyId);

            var Internship = _repositoryManager.Intership.GetInternshipById(InternshipId, false);
            if (Internship == null)
                throw new InternshipNotFoundException(InternshipId);


            var internshipPosition = _mapper.Map<InternshipPosition>(internshipPositionDto);

                _repositoryManager.InternshipPosition.CreateInternshipPosition(internshipPosition);
               await  _repositoryManager.SaveAsync();



            return internshipPosition;




        }

        public async Task DeleteInternshipPosition(string CompanyId  , int InternshipId, int PositionId)
        {
            var company = _repositoryManager.Company.GetCompany(CompanyId, false);
            if (company == null)
                throw new CompanyNotFoundException(CompanyId);

            var Internship = _repositoryManager.Intership.GetInternshipById(InternshipId, false);
            if (Internship == null)
                throw new InternshipNotFoundException(InternshipId);

               var InternshipPosition =   _repositoryManager.InternshipPosition.GetInternshipPosition(InternshipId, PositionId);

            if (InternshipPosition == null)

                throw new InternshipPositionNotFoundException();


            _repositoryManager.InternshipPosition.DeleteInternshipPosition(InternshipId, PositionId);
                await _repositoryManager.SaveAsync();

     

           



        
        }
        public  async Task UpdateInternshipPosition(string CompanyId, int InternshipId, int PositionId, InternshipPositionUpdateDto internshipupdatePosition)
        {
            var company = _repositoryManager.Company.GetCompany(CompanyId, false);
            if (company == null)
                throw new CompanyNotFoundException(CompanyId);

            var Internship = _repositoryManager.Intership.GetInternshipById(InternshipId, false);
            if (Internship == null)
                throw new InternshipNotFoundException(InternshipId);

            var internshipPosition = _repositoryManager.InternshipPosition.GetInternshipPosition(InternshipId, PositionId);

            if (internshipPosition == null)

                throw new InternshipPositionNotFoundException();



            if (!string.IsNullOrEmpty(internshipupdatePosition.Requirements))
                internshipPosition.Requirements = internshipupdatePosition.Requirements;

            if (internshipupdatePosition.NumOfOpenings >= 0)
                internshipPosition.NumOfOpenings = internshipupdatePosition.NumOfOpenings;

           
                await _repositoryManager.SaveAsync();
           
        }
    }


        
    
}
