using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OpportunityService : IOpportunityService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public OpportunityService( IRepositoryManager repositoryManager , IMapper mapper) 
        { 
            _repositoryManager  = repositoryManager;
            _mapper = mapper;
        }

       public async Task<string> SavedOpportunity(SavedOpportunityDto savedOpportunityDto)
        {

              var result= _repositoryManager.OpportunityRepository.GetSavedOpportunity(savedOpportunityDto.StudentId, savedOpportunityDto.PostId, savedOpportunityDto.Type).Result;


            if (result == null)
            {
                var post = _repositoryManager.Intership.GetInternshipById(savedOpportunityDto.PostId, false);
                if (post == null)
                    return ("there is no post for this id");
                try
                {
                    var savedPost = _mapper.Map<SavedPost>(savedOpportunityDto);
                    await _repositoryManager.OpportunityRepository.SavedOpportunity(savedPost);
                    await _repositoryManager.SaveAsync();
                    return "created";
                }
                catch (Exception ex)
                {

                    return ($"Failed Create or mismatch Ids ");

                }
            }
            else
            {
                return ($"Post already Saved ");


            }
        }
        public async Task<string> DeleteOpportunity(SavedOpportunityDto savedOpportunityDto)
        {
            var result = _repositoryManager.OpportunityRepository.GetSavedOpportunity(savedOpportunityDto.StudentId, savedOpportunityDto.PostId, savedOpportunityDto.Type).Result;
            if (result != null)
            {
               var post =  _repositoryManager.Intership.GetInternshipById(savedOpportunityDto.PostId ,false);
                if (post == null)
                    return ("there is no post for this id");
                try
                {
                    var savedPost = _mapper.Map<SavedPost>(savedOpportunityDto);
                    await _repositoryManager.OpportunityRepository.DeleteOpportunity(savedPost);
                    await _repositoryManager.SaveAsync();
                    return "Deleted";
                }
                catch (Exception ex)
                {

                    return ($"Failed Delete  or mismatch Ids");


                }
            }
            else
            {
                return ($"There Is No Post For this Ids");

            }

        }
    }
}
