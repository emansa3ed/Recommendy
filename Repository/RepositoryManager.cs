using Contracts;
using Entities.Models;
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

        public RepositoryManager(RepositoryContext repositoryContext , UserManager<Entities.Models.User> userManager)
        {
            _repositoryContext = repositoryContext;

            _countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(repositoryContext));
            _studentRepository = new Lazy<IStudentRepository>(() => new StudentRepository(repositoryContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext,userManager));
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));
            _universityRepository = new Lazy<IUniversityRepository>(() => new UniversityRepository(repositoryContext));
        }

        public ICountryRepository Country => _countryRepository.Value;
        public IStudentRepository Student => _studentRepository.Value;  
         public IUserRepository User => _userRepository.Value;
        public ICompanyRepository Company => _companyRepository.Value;

        public IUniversityRepository university => _universityRepository.Value; 
        public void Save() => _repositoryContext.SaveChanges();

    }
}
