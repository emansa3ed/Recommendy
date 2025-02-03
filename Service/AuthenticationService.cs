using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
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

          if (userForRegistration.Roles.Any(role => role.Equals("University", StringComparison.OrdinalIgnoreCase) ||  role.Equals("Company", StringComparison.OrdinalIgnoreCase)))
            {
                
                if (string.IsNullOrEmpty(userForRegistration.Url))
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "UrlRequired",
                        Description = "URL is required for university or company registration."
                    });
                }

                if (!Uri.TryCreate(userForRegistration.Url, UriKind.Absolute, out _))
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "InvalidUrl",
                        Description = "Invalid URL format for University or Company URL."
                    });
                }

                
            }

            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
                var roles = await _userManager.GetRolesAsync(user);
                user.Discriminator = roles.FirstOrDefault();
                await _userManager.UpdateAsync(user);
                ////////////////// photo 
               
                string url = _repository.File.UploadImage("Uploads", userForRegistration.UserImage).Result;            
                user.UrlPicture = url;

                await _userManager.UpdateAsync(user);
                ///////////////////////////////////////
                if (roles.Any(role => role.Equals("Student", StringComparison.OrdinalIgnoreCase)))
                {

                    Student student = new Student();
                    student.StudentId = user.Id;
                    _repository.Student.CreateStudent(student);
                    _repository.SaveAsync();


                }
                else if (roles.Any(role => role.Equals("Company", StringComparison.OrdinalIgnoreCase)))
                {


                    Company company = new Company();
                    company.CompanyId = user.Id;
                    company.CompanyUrl = userForRegistration.Url;
                    _repository.Company.CreateCompany(company);
                    _repository.SaveAsync();



                }
                else if (roles.Any(role => role.Equals("University", StringComparison.OrdinalIgnoreCase)))
                {
                    University university = new University();
                    university.UniversityId = user.Id;
                    university.UniversityUrl = userForRegistration.Url;
                    _repository.university.CreateUniversity(university);
                    _repository.SaveAsync();
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError ////solve later
                    {
                      //  Code = "InvalidRole",
                      //  Description = "The specified role is not allowed. Allowed roles are: Student, Company, University."
                    });

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