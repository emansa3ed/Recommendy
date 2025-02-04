using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class InternshipService : IInternshipService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public InternshipService(IRepositoryManager repositoryManager , IMapper mapper) { 
          
            _repositoryManager = repositoryManager;
            _mapper = mapper;


        }



        public  async Task<ApiResponse<Internship>> CreateInternship(InternshipCreationDto internshipDto) {

            try
            {
                var internship =  _mapper.Map<Internship>(internshipDto);

                string url = _repositoryManager.File.UploadImage("Internships", internshipDto.Image).Result;
                 internship.UrlPicture = url;

                 _repositoryManager.Intership.CreateIntership(internship);
               
                 await _repositoryManager.SaveAsync();
                return new ApiResponse<Internship>
                {
                    Success = true,
                    Message = "Internship created successfully.",
                    Data = internship
                };


            }
            catch (Exception ex)
            {
                return new ApiResponse<Internship>
                {
                    Success = false,
                    Message = $"Failed to create internship. {ex.Message} | Inner Exception: {ex.InnerException?.Message}",
                    Data = null
                };
            }

        }
        public  async Task<ApiResponse<int>> DeleteInternship(int Id , bool trackChanges) {

            try
            {

                _repositoryManager.Intership.DeleteIntership(Id , trackChanges);
                await _repositoryManager.SaveAsync();

            }
            catch (Exception ex) {


                return new ApiResponse<int>
                {

                    Success = false,
                    Message = $"Failed to delete internship. {ex.Message} | Inner Exception: {ex.InnerException?.Message}",
                    Data = -1
                };
            
            
            }
            return new ApiResponse<int>
            {

                Success = true,
                Message = $" deleted internship.",
                Data = 1
            };




        }

        public async Task<bool> UpdateInternship(int id, InternshipUpdateDto internshipDto)
        {
          
            var existingInternship =  _repositoryManager.Intership.GetInternshipById(id, true);
            if (existingInternship == null)
            {
                return false; 
            }



            if (!string.IsNullOrEmpty(internshipDto.Name)) 
                existingInternship.Name = internshipDto.Name; 

            if (!string.IsNullOrEmpty(internshipDto.UrlApplicationForm))
                existingInternship.UrlApplicationForm = internshipDto.UrlApplicationForm;

            if (internshipDto.ApplicationDeadline.HasValue)
                existingInternship.ApplicationDeadline = internshipDto.ApplicationDeadline.Value;

            if (!string.IsNullOrEmpty(internshipDto.Description))
                existingInternship.Description = internshipDto.Description;

            if (internshipDto.Paid.HasValue)
                existingInternship.Paid = internshipDto.Paid.Value;

            if (!string.IsNullOrEmpty(internshipDto.Approach))
                existingInternship.Approach = internshipDto.Approach;

            if (internshipDto.Image != null)
            {
                var url = await _repositoryManager.File.UploadImage("Internships", internshipDto.Image);

                existingInternship.UrlPicture = url;
            }

            try
            {
                 _repositoryManager.Save();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update internship. {ex.Message} | Inner Exception: {ex.InnerException?.Message}");
            }
        }
    



    public async Task<List<InternshipDto>> GetInternshipsByCompanyId(string companyId)
        {
            var internships = await _repositoryManager.Intership.GetInternshipsByCompanyId(companyId, trackChanges: false);
            List<InternshipDto> result = _mapper.Map<List<InternshipDto>>(internships);
            return result;
        }

        public async Task<InternshipDto> GetInternshipById(int id, bool trackChanges)
        {
            try
            {
                var internship =  _repositoryManager.Intership.GetInternshipById(id, trackChanges);
                var internshipDto = _mapper.Map<InternshipDto>(internship);
                return internshipDto;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving scholarship", ex);
            }
        }


        //
        public async Task<IEnumerable<InternshipDto>> GetAllInternships(bool trackChanges)
        {
            try
            {
                var internships = _repositoryManager.Intership.GetAllInternships(trackChanges);
                var internshipDto = _mapper.Map<IEnumerable<InternshipDto>>(internships);
                return await Task.FromResult(internshipDto);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while fetching Internships.");
            }
        }

       



    }
}
