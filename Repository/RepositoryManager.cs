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
        private readonly Lazy<IScholarshipRepository> _scholarshipReposiyory;
        private readonly Lazy<IFileRepository> _fileRepository;
        private readonly Lazy<IInternshipRepository> _intershipRepository;
        private readonly Lazy<IInternshipPositionRepository> _internshipPositionRepository;
        private readonly Lazy<IPositionRepository> _positionRepository;
        private readonly Lazy<IUserCodeRepository> _userCodeRepository;
        private readonly Lazy<IOpportunityRepository>  _opportunityRepository;
        private readonly Lazy<IFeedbackRepository>  _feedbackRepository;
        private readonly Lazy<IReportRepository> _reportRepository;
		public RepositoryManager(RepositoryContext repositoryContext , UserManager<User> userManager , IWebHostEnvironment webHostEnvironment)
        {
            _repositoryContext = repositoryContext;

            _countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(repositoryContext));
            _studentRepository = new Lazy<IStudentRepository>(() => new StudentRepository(repositoryContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext,userManager));
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));
            _universityRepository = new Lazy<IUniversityRepository>(() => new UniversityRepository(repositoryContext));
            _scholarshipReposiyory = new Lazy<IScholarshipRepository>(() => new ScholarshipRepository(repositoryContext));
            _fileRepository = new Lazy<IFileRepository>(() => new FileRepository(webHostEnvironment));
            _intershipRepository = new Lazy<IInternshipRepository>(() => new InternshipRepository(repositoryContext));
            _internshipPositionRepository = new Lazy<IInternshipPositionRepository>(()=> new InternshipPositionRepository(repositoryContext));
            _positionRepository= new Lazy<IPositionRepository>(() => new PositionRepository(repositoryContext));
         
             _userCodeRepository = new Lazy<IUserCodeRepository>(() => new UserCodeRepository(repositoryContext));
            _opportunityRepository = new Lazy<IOpportunityRepository>(() => new OpportunityRepository(repositoryContext));
			_feedbackRepository = new Lazy<IFeedbackRepository>(() => new FeedbackRepository(repositoryContext));
            _reportRepository = new Lazy<IReportRepository>(()=> new ReportRepository(repositoryContext));
		}

        public ICountryRepository Country => _countryRepository.Value;
        public IStudentRepository Student => _studentRepository.Value;  
         public IUserRepository User => _userRepository.Value;
        public ICompanyRepository Company => _companyRepository.Value;
        public IUniversityRepository university => _universityRepository.Value; 

        public IScholarshipRepository Scholarship => _scholarshipReposiyory.Value;
        public IFileRepository File => _fileRepository.Value;  
        
        public IInternshipRepository Intership => _intershipRepository.Value;

        public IInternshipPositionRepository InternshipPosition => _internshipPositionRepository.Value;

        public IPositionRepository PositionRepository => _positionRepository.Value;
        public IUserCodeRepository UserCodeRepository => _userCodeRepository.Value;

        public IOpportunityRepository OpportunityRepository => _opportunityRepository.Value;

		public IFeedbackRepository FeedbackRepository => _feedbackRepository.Value;

        public IReportRepository ReportRepository => _reportRepository.Value;
		public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
        public void Save() => _repositoryContext.SaveChanges();


    }
}
