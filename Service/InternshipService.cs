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
        public Task DeleteInternship(Internship internship) {

            try
            {

                _repositoryManager.Intership.DeleteIntership(internship);
                _repositoryManager.SaveAsync().Wait();

            }
            catch (Exception ex) { throw; }

            return Task.CompletedTask;
        
        }

        public Task UpdateInternship(Internship internship) {
            try
            {
                _repositoryManager.Intership.UpdateIntership(internship);
                _repositoryManager.SaveAsync().Wait();
            }
            catch (Exception ex) { throw; }


            return Task.CompletedTask;
        
        }



        public async Task<List<InternshipDto>> GetInternshipsByCompanyId(string companyId)
        {
            var internships = await _repositoryManager.Intership.GetInternshipsByCompanyId(companyId, trackChanges: false);
           List<InternshipDto> result = _mapper.Map<List<InternshipDto>>(internships);
            return result;
        }



    }
}
