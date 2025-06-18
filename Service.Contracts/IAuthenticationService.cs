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
        Task<TokenDto> HandleGoogleLoginAsync(string email, string name, string? picture,  string? firstName = null, string? lastName = null);
        Task<TokenDto> HandleGoogleIdTokenAsync(string idToken);
        Task<TokenDto> HandleGoogleLoginOnlyAsync(string idToken);


		string ExtractUserIdFromToken(string token);
    }
}
