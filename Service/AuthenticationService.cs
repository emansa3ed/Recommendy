using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Transactions;
using Shared.DTO.Authentication;
using Entities.ResumeModels;
using Google.Apis.Auth;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {

        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserCodeService _userCodeService;
        private readonly IResumeParserService _resumeParserService;
        private readonly HttpClient _httpClient;


        private User? _user;

        public AuthenticationService(IMapper mapper, UserManager<User> userManager,
            IConfiguration configuration, IRepositoryManager repository, IHttpContextAccessor httpContextAccessor, IUserCodeService userCodeService, IResumeParserService resumeParserService, HttpClient httpClient)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _userCodeService = userCodeService;
            _httpClient = httpClient;
            _resumeParserService = resumeParserService;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration, HttpContext HttpContext)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                if (userForRegistration.Roles.Any(role => role.Equals("University", StringComparison.OrdinalIgnoreCase) || role.Equals("Company", StringComparison.OrdinalIgnoreCase)))
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
                else if (userForRegistration.Roles.Any(role => role.Equals("Student", StringComparison.OrdinalIgnoreCase)))
                {

                }
                else
                {

                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "InvalidRole",
                        Description = "The specified role is not allowed. Allowed roles are: Student, Company, University."
                    });

                }

                var user = _mapper.Map<User>(userForRegistration);
                var result = await _userManager.CreateAsync(user, userForRegistration.Password);
                var roleRes = await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
                var roles = await _userManager.GetRolesAsync(user);
                if (result.Succeeded)
                {
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
                        if (userForRegistration.ResumeFile != null)
                        {
                            student.Skills = string.Join(",", await _resumeParserService.UploadResume(userForRegistration.ResumeFile));
                        }
                        student.StudentId = user.Id;
                        _repository.Student.CreateStudent(student);
                        _repository.SaveAsync().Wait();

                    }
                    else if (roles.Any(role => role.Equals("Company", StringComparison.OrdinalIgnoreCase)))
                    {


                        Company company = new Company();
                        company.CompanyId = user.Id;
                        company.CompanyUrl = userForRegistration.Url;
                        _repository.Company.CreateCompany(company);
                        _repository.SaveAsync().Wait();



                    }
                    else if (roles.Any(role => role.Equals("University", StringComparison.OrdinalIgnoreCase)))
                    {
                        University university = new University();
                        university.UniversityId = user.Id;
                        university.UniversityUrl = userForRegistration.Url;
                        var CounryName = await GetCounryName(HttpContext);
                        var contries = await _repository.Country.GetAllCountriesAsync(false);
                        var res = contries.Where(c => c.Name == CounryName).SingleOrDefault();
                        if (res != null)
                            university.CountryId = res.Id;
                        else
                        {
                            Country country = new Country();
                            country.Name = CounryName;
                            _repository.Country.CreateCountry(country);
                            university.Country = country;
                        }
                        _repository.university.CreateUniversity(university);
                        await _repository.SaveAsync();
                    }

                }
                else
                    return result;

                var result1 = await _userCodeService.GenerateUserCodeForConfirmtationAsync(user.Id);
                if (!roleRes.Succeeded)
                    return roleRes;
                transaction.Complete();
                return result;

            }
        }

        private async Task<string> GetCounryName(HttpContext HttpContext)
        {
            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? HttpContext.Connection.RemoteIpAddress?.ToString();
            //ip = "101.46.224.0"; //test with Germany ip
            if (ip == "::1" || ip == "127.0.0.1") // Handle localhost
                return "Egypt";
            var url = $"http://ip-api.com/json/{ip}";
            var response = await _httpClient.GetStringAsync(url);
            var json = JsonDocument.Parse(response);

            var country = json.RootElement.GetProperty("country").GetString();
            var city = json.RootElement.GetProperty("city").GetString();
            return country;
        }

        private string GenerateRandomNumericToken(int length = 8)
        {
            var random = new Random();
            var token = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                token.Append(random.Next(0, 10));
            }

            return token.ToString();
        }
        ////////////////////////////////// TTTTOOOO Login
        public async Task<bool> ValidateUser(UserForLoginDto userForLogin)
        {
            _user = await _userManager.FindByNameAsync(userForLogin.UserName);

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForLogin.Password));


            return result;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var refreshToken = GenerateRefreshToken();

            _user.RefreshToken = refreshToken;

            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(30);

            await _userManager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenDto(accessToken, refreshToken); ;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JWT");
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SecretKey"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
           {
            new Claim(ClaimTypes.Name, _user.UserName),
            new Claim("Id", _user.Id) // Add the user ID as a claim
           };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JWT");
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);

            }
        }
        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new RefreshTokenBadRequest();
            _user = user;
            return await CreateToken(populateExp: false);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JWT");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SecretKey"))),
                ValidateLifetime = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
        !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }



        public string ExtractUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
            {
                throw new ArgumentException("Invalid JWT token");
            }

            var jwtToken = handler.ReadJwtToken(token);


            var userId = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID claim not found in the token");
            }

            return userId;
        }

        public async Task<TokenDto> HandleGoogleLoginAsync(string email, string name, string? picture, string? firstName = null, string? lastName = null)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName ?? name?.Split(' ')[0],
                    LastName = lastName ?? (name?.Split(' ').Length > 1 ? name.Split(' ')[1] : ""),
                    EmailConfirmed = true,
                    UrlPicture = picture,
                    Discriminator = "Student"
                };
                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                    throw new Exception("Failed to create user account");
                await _userManager.AddToRoleAsync(user, "Student");
            }
          
            _user = user;
            Student student = new Student();
            student.StudentId = user.Id;
            _repository.Student.CreateStudent(student);
            _repository.SaveAsync().Wait();
            return await CreateToken(populateExp: true);
        }

        public async Task<TokenDto> HandleGoogleIdTokenAsync(string idToken, string? firstName = null, string? lastName = null)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            return await HandleGoogleLoginAsync(payload.Email,payload.Name, payload.Picture, firstName, lastName);
        }

        public async Task<TokenDto> HandleGoogleLoginOnlyAsync(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
                throw new UnauthorizedAccessException("User not found or not registered with Google.");
            _user = user;
            return await CreateToken(populateExp: true);
        }
    }
}