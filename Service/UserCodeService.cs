using Contracts;
using Entities.Exceptions;
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

       public async Task<string> GenerateUserCodeForConfirmtationAsync(string UserId)
        {
            



            
                User user =   await _userManager.FindByIdAsync(UserId);
                if (user == null) {

                    throw new UserNotFoundException(UserId);


                }

                if (user.EmailConfirmed == true) return "User already confirmed ";

               int NumOFCodes  =    await _repository.UserCodeRepository.GetNumByIdAsync(UserId);
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

                var subject = "Email Confirmation Required";
                var body = $@"
    <p>Dear {user?.UserName},</p>
    <p>Thank you for registering. To complete your email verification, please use the confirmation code below:</p>
    <h2 style='color: #2c3e50; text-align: center;'>{token}</h2>
    <p>If you did not request this, please ignore this email.</p>
    <p>Best regards,<br>Your Support Team</p>";


                var result = await _EmailsService.Sendemail(user.Email, body, subject);
                if (result == "Email sended")
                    return "Email sended";
                else return result;
           
          

        }

        public async Task<string> GenerateUserCodeForResetPasswordAsync(string UserId)
        {

           
                User user =  await  _userManager.FindByIdAsync(UserId);
                if (user == null)
                {

                    throw new UserNotFoundException(UserId);

                }



                int NumOFCodes = await _repository.UserCodeRepository.GetNumByIdAsync(UserId);
                if (NumOFCodes >= 1)
                {
                    return "A confirmation code was already sent. Please check your email.";

                }
                var code = GenerateRandomNumericToken();
                UserCode userCode = new UserCode();
                userCode.UserId = UserId;
                userCode.Token = code;
                userCode.ExpirationDate = DateTime.UtcNow.AddMinutes(3);
                await _repository.UserCodeRepository.AddAsync(userCode);
                await _repository.SaveAsync();

                string subject = "Password Reset Request";


                string message = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    text-align: center;
                    padding: 20px;
                }}
                .container {{
                    background-color: #ffffff;
                    padding: 20px;
                    border-radius: 10px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    display: inline-block;
                }}
                .code {{
                    font-size: 24px;
                    font-weight: bold;
                    color: #2c3e50;
                    margin: 20px 0;
                }}
                .footer {{
                    margin-top: 20px;
                    font-size: 12px;
                    color: #888;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Password Reset Request</h2>
                <p>You are receiving this email because you requested a password reset.</p>
                <p>Please use the following verification code to complete the process:</p>
                <p class='code'>{code}</p>
                <p>This code is valid for <b>3 minutes</b>. Do not share it with anyone.</p>
                <p>If you did not request a password reset, you can safely ignore this email.</p>
            </div>
            <p class='footer'>© {DateTime.UtcNow.Year} Support Team</p>
        </body>
        </html>";

                var result = await _EmailsService.Sendemail(user.Email, message, subject);
                if (result == "Email Sended")
                    return "Email Sended";
                else return result;
           
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
