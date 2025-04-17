using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Entities.Models;
using Shared.DTO;
using Entities.Exceptions;

namespace Service
{
    public class EmailsService : IEmailsService
    {
        
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;


        public EmailsService(IConfiguration configuration , IRepositoryManager repository , UserManager<User> userManager)
        {
               _configuration = configuration;
               _repository =  repository;
            _userManager = userManager;
        }

        public async Task<string> Sendemail(string email, string Message, string? reason)
        {
            var EmailSettings = _configuration.GetSection("emailSettings");

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);

					client.Authenticate(EmailSettings["FromEmail"], Environment.GetEnvironmentVariable("EmailPassword"));
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "wellcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Recommendy Team", EmailSettings["FromEmail"]));
                    message.To.Add(new MailboxAddress("testing", email));
                    message.Subject = reason == null ? "No Submitted" : reason;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
             
                return "Email Sended";
            }
            catch (Exception ex)
            {
                return $"Failed. {ex.Message} | Inner Exception: {ex.InnerException?.Message}";
            }
        }
      
        public async Task<IActionResult> ConfirmEmailAsync(string  userId, string token)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw  new UserNotFoundException(userId);
            }

            var userToken = await _repository.UserCodeRepository.GetAsync(userId, token);
            if (userToken == null || userToken.ExpirationDate < DateTime.UtcNow)
            {
                return new BadRequestObjectResult("Code not found or expired");
            }

            try
            {
                // Confirm the email
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);

                await _repository.UserCodeRepository.DeleteAsync(userToken);  ///// welcome
               await _repository.SaveAsync();
                return new OkObjectResult("Email confirmed successfully");
            }
            catch (Exception ex) {

                return new BadRequestObjectResult($"Error confirming email. {ex.Message} | Inner Exception: {ex.InnerException?.Message}");
                }
        }

        public async Task<string> ConfirmationForResetPasswordAsync(string userId, string token,string NewPassword)
        {
            var userToken = await _repository.UserCodeRepository.GetAsync(userId, token);
            if (userToken == null || userToken.ExpirationDate < DateTime.UtcNow)
            {
                return new ("Code not found or expired");
            }


            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ("User not found");
            }

            try
            {
                var ResetResult = await _userManager.RemovePasswordAsync(user);
                if (!ResetResult.Succeeded)
                {
                    return $"Failed to reset password: {string.Join(", ", ResetResult.Errors.Select(e => e.Description))}";
                }

                var AddPasswordResult = await _userManager.AddPasswordAsync(user, NewPassword);
                if (!AddPasswordResult.Succeeded)
                {
                    return $"Failed to set new password: {string.Join(", ", AddPasswordResult.Errors.Select(e => e.Description))}";
                }
                await _userManager.UpdateAsync(user);


                await _repository.UserCodeRepository.DeleteAsync(userToken);  
                await _repository.SaveAsync();
                return new ("Password reset successfully");
            }
            catch (Exception ex)
            {

                return new($"Error confirming email. {ex.Message} | Inner Exception: {ex.InnerException?.Message}");
            }
        }

    }
}
