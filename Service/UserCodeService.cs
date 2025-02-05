using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserCodeService : IUserCodeService
    {
        private readonly IRepositoryManager _repository;
        private readonly IEmailsService _EmailsService;
        private readonly UserManager<User> _userManager;
        public UserCodeService(IRepositoryManager repositoryManager, IEmailsService EmailsService , UserManager<User> userManager) { 

            _repository = repositoryManager;
            _EmailsService = EmailsService;
            _userManager = userManager;
        
        }

       public async Task<string> GenerateUserCodeAsync(string UserId)
        {


            try
            {
                User user =   _userManager.FindByIdAsync(UserId).Result;
                if (user == null) {

                    return $"there is no user for this id  .";

                }
               int NumOFCodes  =  _repository.UserCodeRepository.GetNumByIdAsync(UserId);
                if (NumOFCodes >= 1)
                {
                    return "A confirmation code was already sent. Please check your email.";

                }
                var token = GenerateRandomNumericToken();
                UserCode userCode = new UserCode();
                userCode.UserId = UserId;
                userCode.Token = token;
                userCode.ExpirationDate = DateTime.UtcNow.AddMinutes(3);
                await _repository.UserCodeRepository.AddAsync(userCode);
                await _repository.SaveAsync();

                var result = await _EmailsService.SendConfirmationEmailAsync(user.Email, token);
                if (result == "Email sended")
                    return "Email sended";
                else return result;
            }
            catch (Exception ex)
            {
                return $"Error Generate UserCode . {ex.Message} | Inner Exception: {ex.InnerException?.Message}";

            }
          

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

    }
}
