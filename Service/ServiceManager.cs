using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICountryService> _countryService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IInternshipService> _internshipService;
        private readonly Lazy<IInternshipPositionService> _internshipPositionService;
        private readonly Lazy<IPositionService> _positionService;


        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager,IConfiguration configuration,  IWebHostEnvironment _webHostEnvironment )
        {
            _countryService = new Lazy<ICountryService>(() => new  CountryService(repositoryManager, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, userManager, configuration, repositoryManager));
           _userService = new Lazy<IUserService>(()=>  new UserService(repositoryManager,userManager));
            _internshipService =    new Lazy<IInternshipService>(() =>  new InternshipService(repositoryManager , mapper));
            _internshipPositionService = new Lazy<IInternshipPositionService>(() => new InternshipPositionService(repositoryManager ,mapper));  
            _positionService = new Lazy<IPositionService>(()=> new PositionService(repositoryManager));
        } 



        public ICountryService CountryService => _countryService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IUserService UserService => _userService.Value;
        public IInternshipService InternshipService => _internshipService.Value;
        public IInternshipPositionService InternshipPosition => _internshipPositionService.Value;
       
        public IPositionService PositionService => _positionService.Value;
    }
}
