using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.DTO.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration, HttpContext HttpContext);
        Task<bool> ValidateUser(UserForLoginDto userForLogin);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);


		string ExtractUserIdFromToken(string token);
    }
}
