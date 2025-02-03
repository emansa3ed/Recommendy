using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service
{
    public class InternshipPositionService : IInternshipPositionService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public InternshipPositionService( IRepositoryManager repositoryManager , IMapper mapper  ) { 

            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }


        public  async Task<ApiResponse<int>> CreateInternshipPosition(InternshipPositionDto internshipPositionDto)
        {

            try
            {
                var internshipPosition =   _mapper.Map<InternshipPosition>(internshipPositionDto);

                 _repositoryManager.InternshipPosition.CreateInternshipPosition(internshipPosition);
                 _repositoryManager.SaveAsync().Wait();
            }
            catch (Exception ex) {

                return new ApiResponse<int>
                {
                    Success = false,
                    Message = "Failed to create internshipPosition. Please try again later." + ex.Message,
                  

                };
            
            
            }


            return new ApiResponse<int>
            {
                Success = true,
                Message = " created internshipPosition. "  ,
             


            };




        }


    }
}
