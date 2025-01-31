using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ICountryRepository> _countryRepository;
        private readonly Lazy<IStudentRepository> _studentRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IUniversityRepository> _universityRepository;
        private readonly Lazy<IScholarshipReposiyory> _scholarshipReposiyory;
        private readonly Lazy<IFileRepository> _fileRepository;

        public RepositoryManager(RepositoryContext repositoryContext , UserManager<User> userManager , IWebHostEnvironment webHostEnvironment)
        {
            _repositoryContext = repositoryContext;

            _countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(repositoryContext));
            _studentRepository = new Lazy<IStudentRepository>(() => new StudentRepository(repositoryContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext,userManager));
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));
            _universityRepository = new Lazy<IUniversityRepository>(() => new UniversityRepository(repositoryContext));
            _scholarshipReposiyory = new Lazy<IScholarshipReposiyory>(() => new ScholarshipReposiyory(repositoryContext));
            _fileRepository = new Lazy<IFileRepository>(() => new FileRepository(webHostEnvironment));
        }

        public ICountryRepository Country => _countryRepository.Value;
        public IStudentRepository Student => _studentRepository.Value;  
         public IUserRepository User => _userRepository.Value;
        public ICompanyRepository Company => _companyRepository.Value;
        public IUniversityRepository university => _universityRepository.Value; 

        public IScholarshipReposiyory scholarship => _scholarshipReposiyory.Value;
        public IFileRepository File => _fileRepository.Value;   
        public void Save() => _repositoryContext.SaveChanges();

    }
}
