using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.GeneralResponse;
using Entities.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Service.Contracts;
using Service.Ontology;
using Shared.DTO.Feedback;
using Shared.DTO.Internship;
using Shared.DTO.Report;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class InternshipService : IInternshipService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
		private readonly MyMemoryCache _memoryCache;

		public InternshipService(IRepositoryManager repositoryManager , IMapper mapper, MyMemoryCache memoryCache) { 
          
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _memoryCache = memoryCache;
		}

        public  async Task<Internship> CreateInternship(  string CompanyId ,InternshipCreationDto internshipDto) 
        {

           var company  = _repositoryManager.Company.GetCompany(CompanyId,false);
            if (company == null)
                throw  new CompanyNotFoundException(CompanyId);

           
                var internship =  _mapper.Map<Internship>(internshipDto);

                string url = _repositoryManager.File.UploadImage("Internships", internshipDto.Image).Result;
                 internship.UrlPicture = url;
                 internship.CompanyId = CompanyId;

                 _repositoryManager.Intership.CreateIntership(internship);
               
                 await _repositoryManager.SaveAsync();

            return internship;


             

        }
        public  async Task DeleteInternship( string CompanyId, int Id , bool trackChanges) 
        {
            var company = _repositoryManager.Company.GetCompany(CompanyId, false);
            if (company == null)
                throw new CompanyNotFoundException(CompanyId);

            var Internship = _repositoryManager.Intership.GetInternshipById(CompanyId, Id, false);
            if (Internship == null)
                throw new InternshipNotFoundException(Id);


            _repositoryManager.Intership.DeleteIntership(Id , trackChanges);
                await _repositoryManager.SaveAsync();

        }

        public async Task UpdateInternship( string CompanyId,  int id, InternshipUpdateDto internshipDto)
        {
          
            var existingInternship =  _repositoryManager.Intership.GetInternshipById(CompanyId,id, true);
            if (existingInternship == null)
              throw new InternshipNotFoundException(id);
            
            var company = _repositoryManager.Company.GetCompany(CompanyId, false);
            if (company == null)
                throw new CompanyNotFoundException(CompanyId);

           



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
           
                 _repositoryManager.Save();
          
        }
    



        public async Task<PagedList<InternshipDto>> GetInternshipsByCompanyId(string CompanyId , InternshipParameters internshipParameters)
        {
            var company = _repositoryManager.Company.GetCompany(CompanyId, false);
            if (company == null)
                throw new CompanyNotFoundException(CompanyId);

            var internships = await _repositoryManager.Intership.GetInternshipsByCompanyId(CompanyId,internshipParameters, trackChanges: false);
            List<InternshipDto> result = _mapper.Map<List<InternshipDto>>(internships);

            return new PagedList<InternshipDto>(result, internships.MetaData.TotalCount, internshipParameters.PageNumber, internshipParameters.PageSize);

        }



        public async Task<InternshipDto> GetInternshipById(int id, bool trackChanges)
        {
            
               var internship =  _repositoryManager.Intership.InternshipById(id, trackChanges);
            if (internship == null)
                throw new InternshipNotFoundException(id);

                var internshipDto = _mapper.Map<InternshipDto>(internship);
                return internshipDto;

         
        }


      
        public async Task<PagedList<InternshipDto>> GetAllInternships(InternshipParameters internshipParameters, bool trackChanges)

        {

           if (!_memoryCache.Cache.TryGetValue(internshipParameters.ToString()+ "GetAllInternships", out PagedList<Internship> cacheValue))
			{
				cacheValue = await _repositoryManager.Intership.GetAllInternshipsAsync(internshipParameters, trackChanges);

                var jsonSize = JsonSerializer.SerializeToUtf8Bytes(cacheValue).Length;


				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSize(jsonSize)
					.SetSlidingExpiration(TimeSpan.FromSeconds(5))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

				_memoryCache.Cache.Set(internshipParameters.ToString() + "GetAllInternships", cacheValue, cacheEntryOptions);
			}
            var internships = cacheValue;

			var internshipDto = _mapper.Map<List<InternshipDto>>(internships);
			return new PagedList<InternshipDto>(internshipDto, internships.MetaData.TotalCount, internshipParameters.PageNumber, internshipParameters.PageSize); 
        }

		public async Task<PagedList<InternshipDto>> GetAllRecommendedInternships(string UserSkills, InternshipParameters internshipParameters, bool trackChanges)
		{
			if (!_memoryCache.Cache.TryGetValue(internshipParameters.ToString()+UserSkills+ "GetAllRecommendedInternships", out PagedList<Internship> cacheValue))
			{

				var skills = UserSkills
			                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			                .Select(term => term.ToLower())
			                .ToList();
				var ExpandedSkills = SkillOntology.ExpandSkills(skills);
				var NewSkills = string.Join(",", ExpandedSkills.ToList());
				cacheValue = await _repositoryManager.Intership.GetAllRecommendedInternships(NewSkills, internshipParameters, trackChanges);

				var jsonSize = JsonSerializer.SerializeToUtf8Bytes(cacheValue).Length;


				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSize(jsonSize)
					.SetSlidingExpiration(TimeSpan.FromSeconds(30))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(120));

				_memoryCache.Cache.Set(internshipParameters.ToString() + UserSkills+ "GetAllRecommendedInternships", cacheValue, cacheEntryOptions);
			}
			var internships = cacheValue;

			var internshipDto = _mapper.Map<List<InternshipDto>>(internships);
			return new PagedList<InternshipDto>(internshipDto, internships.MetaData.TotalCount, internshipParameters.PageNumber, internshipParameters.PageSize);
		}
	}
}
