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
                    client.Authenticate(EmailSettings["FromEmail"],EmailSettings["password"]);
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
             
                return "Success";
            }
            catch (Exception ex)
            {
                return $"Failed. {ex.Message} | Inner Exception: {ex.InnerException?.Message}";
            }
        }
        public async Task<string > SendConfirmationEmailAsync(string email, string token)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            var subject = "Email Confirmation Required";
            var body = $@"
    <p>Dear {user?.UserName},</p>
    <p>Thank you for registering. To complete your email verification, please use the confirmation code below:</p>
    <h2 style='color: #2c3e50; text-align: center;'>{token}</h2>
    <p>If you did not request this, please ignore this email.</p>
    <p>Best regards,<br>Your Support Team</p>";



            var result =  await Sendemail(email, body, subject);

            if (result == "Success")
                return "Email sended";
            else return result;
        }
        public async Task<IActionResult> ConfirmEmailAsync(string  userId, string token)
        {
            var userToken = await _repository.UserCodeRepository.GetAsync(userId, token);
            if (userToken == null || userToken.ExpirationDate < DateTime.UtcNow)
            {
                return new BadRequestObjectResult("Token not found or expired");
            }

            var user = await _userManager.FindByIdAsync(userId); 
            if (user == null)
            {
                return new BadRequestObjectResult("User not found");
            }

            try
            {
                // Confirm the email
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);

                await _repository.UserCodeRepository.DeleteAsync(userToken);  ///// welcome
                return new OkObjectResult("Email confirmed successfully");
            }
            catch (Exception ex) {

                return new BadRequestObjectResult($"Error confirming email. {ex.Message} | Inner Exception: {ex.InnerException?.Message}");
                }
        }

    }
}
