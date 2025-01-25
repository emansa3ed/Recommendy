using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {

        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _repository;

        private User? _user;

        public AuthenticationService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration ,IRepositoryManager repository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
                var roles = _repository.User.GetType(user.Id).Result;
                if (roles == "Student")
                {
                    
                        user.Discriminator = roles;
                        Student student = new Student();
                        student.StudentId = user.Id;
                        _repository.Student.CreateStudent(student);
                        _repository.Save();
                   

                }
                else if(roles == "Company")
                {
                   
                        user.Discriminator = roles;
                        Company company = new Company();
                        company.CompanyId = user.Id;
                        _repository.Company.CreateCompany(company);
                        _repository.Save();
                   


                }
               else
                {
                    
                        user.Discriminator = roles;
                        University university = new University();
                        university.UniversityId = user.Id;
                        _repository.university.CreateUniversity(university);
                        _repository.Save();
                    
                }
            }
            return result;
        }
        ////////////////////////////////// TTTTOOOO Login
        public async Task<bool> ValidateUser(UserForLoginDto userForLogin)
        {
            _user = await _userManager.FindByNameAsync(userForLogin.UserName);

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForLogin.Password));
         
           return result;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials() /////////// return secret key as a byte array with the security algorithm
        {
            var jwtSettings = _configuration.GetSection("JWT");
            var key = Encoding.UTF8.GetBytes((jwtSettings["SecretKey"]));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()// creates a list of claims with the user name inside and all the roles the user belongs to
        {
            var claims = new List<Claim>{new Claim(ClaimTypes.Name, _user.UserName)};

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,List<Claim> claims) // creates an object of the JwtSecurityToken 
        {
            var jwtSettings = _configuration.GetSection("JWT");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}