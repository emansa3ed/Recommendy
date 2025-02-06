using AutoMapper;
using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public CompanyService(IRepositoryManager repository, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public CompanyViewDto GetCompany(string id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(id, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(id);
            var companyviewDto = _mapper.Map<CompanyViewDto>(company);
            return companyviewDto;
        }

        public async Task UpdateCompany(string companyId, CompanyDto companyDto, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            if (!string.IsNullOrEmpty(companyDto.CompanyUrl))
            {
                company.CompanyUrl = companyDto.CompanyUrl;
            }
            if (!string.IsNullOrEmpty(companyDto.UserName))
            {
                company.User.UserName = companyDto.UserName;
            }


            if (!string.IsNullOrEmpty(companyDto.Bio))
            {
                company.User.Bio = companyDto.Bio;
            }
            if (!string.IsNullOrEmpty(companyDto.PhoneNumber))
            {
                company.User.PhoneNumber = companyDto.PhoneNumber;
            }

            _repository.Company.UpdateCompany(company);
            _repository.Save();
        }

        public async Task<string> UploadProfilePictureAsync(IFormFile file, string companyId)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: true);
            var imageUrl = await _repository.File.UploadImage("Company", file);
            company.User.UrlPicture = imageUrl;
            _repository.Save();
            return imageUrl;
        }

    }
}
