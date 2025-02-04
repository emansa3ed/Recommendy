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
        public InternshipPositionService(IRepositoryManager repositoryManager, IMapper mapper)
        {

            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }


        public async Task<ApiResponse<int>> CreateInternshipPosition(InternshipPositionDto internshipPositionDto)
        {

            try
            {
                var internshipPosition = _mapper.Map<InternshipPosition>(internshipPositionDto);

                _repositoryManager.InternshipPosition.CreateInternshipPosition(internshipPosition);
                _repositoryManager.SaveAsync().Wait();
            }
            catch (Exception ex)
            {

                return new ApiResponse<int>
                {
                    Success = false,
                    Message = $"Failed to create internshipPosition. {ex.Message} | Inner Exception: {ex.InnerException?.Message}",
                    Data = -1

                };


            }


            return new ApiResponse<int>
            {
                Success = true,
                Message = " created internshipPosition. ",
                Data = 1


            };




        }

        public async Task<ApiResponse<int>> DeleteInternshipPosition(int InternshipId, int PositionId)
        {

            try
            {
                _repositoryManager.InternshipPosition.DeleteInternshipPosition(InternshipId, PositionId);
                await _repositoryManager.SaveAsync();


            }
            catch (Exception ex)
            {
                return new ApiResponse<int>
                {
                    Success = false,
                    Message = $"Failed to Delete internshipPosition. {ex.Message} | Inner Exception: {ex.InnerException?.Message}",
                    Data = -1

                };

            }



            return new ApiResponse<int>
            {
                Success = true,
                Message = " Deleted internshipPosition. ",
                Data = 1


            };

        }
        public  async Task<bool> UpdateInternshipPosition(int IntrenshipId, int PositionId, InternshipPositionUpdateDto internshipupdatePosition)
        {
            var intershipposition = _repositoryManager.InternshipPosition.GetInternshipPosition(IntrenshipId, PositionId);

            if (!string.IsNullOrEmpty(internshipupdatePosition.Requirements))
                intershipposition.Requirements = internshipupdatePosition.Requirements;

            if (internshipupdatePosition.NumOfOpenings >= 0)
                intershipposition.NumOfOpenings = internshipupdatePosition.NumOfOpenings;

            try
            {
                await _repositoryManager.SaveAsync();
                return true;

            }
            catch (Exception ex) 
            {

                throw new Exception($"Failed to edit internshipPosition. {ex.Message} | Inner Exception: {ex.InnerException?.Message}");

            }


        }
    }


        
    
}
