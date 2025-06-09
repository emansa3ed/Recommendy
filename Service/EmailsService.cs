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
using Microsoft.Extensions.Logging;

namespace Service
{
    public class EmailsService : IEmailsService
    {
        
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EmailsService> _logger;


        public EmailsService(IConfiguration configuration , ILogger<EmailsService> logger, IRepositoryManager repository , UserManager<User> userManager)
        {
               _configuration = configuration;
               _repository =  repository;
            _userManager = userManager;
            _logger = logger;
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

       

        public async Task SendVerificationEmail(string email, bool isApproved, string notes)
        {
            var subject = isApproved
                ? "Your Organization Has Been Verified"
                : "Organization Verification Update";

            var message = isApproved
                ? $@"
            <h2>Congratulations!</h2>
            <p>Your organization has been verified by our admin team.</p>
            <p>You can now post opportunities on our platform.</p>
            {(string.IsNullOrEmpty(notes) ? "" : $"<p><strong>Admin Notes:</strong> {notes}</p>")}
            <p>Thank you for joining Recommendy!</p>"
                : $@"
            <h2>Verification Update</h2>
            <p>We're sorry, but your organization could not be verified at this time.</p>
            <p><strong>Reason:</strong> {notes}</p>
            <p>If you believe this is an error, please contact our support team.</p>";

            try
            {
                var emailSettings = _configuration.GetSection("emailSettings");
                using var client = new SmtpClient();

                await client.ConnectAsync("smtp.gmail.com", 465, true);
                client.Authenticate(emailSettings["FromEmail"], Environment.GetEnvironmentVariable("EmailPassword"));

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = message,
                    TextBody = message // Fallback text version
                };

                var emailMessage = new MimeMessage
                {
                    Body = bodyBuilder.ToMessageBody(),
                    From = { new MailboxAddress("Recommendy Team", emailSettings["FromEmail"]) },
                    To = { new MailboxAddress("Organization", email) },
                    Subject = subject
                };

                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send verification email: {ex.Message}");
                throw;
            }
        }

        public async Task SendBanEmail(string email, string reason)
        {
            var message = $@"
            <h2>Account Suspended</h2>
            <p>Your Recommendy account has been suspended.</p>
            <p><strong>Reason:</strong> {reason}</p>
            <p>If you believe this is a mistake, please contact our support team.</p>";

            await SendFormattedEmail(email, "Account Suspended", message);
        }

        public async Task SendUnbanEmail(string email)
        {
            var message = @"
            <h2>Account Reinstated</h2>
            <p>Your account has been reinstated. You can now resume using Recommendy.</p>
            <p>Thank you for your cooperation.</p>";

            await SendFormattedEmail(email, "Account Reinstated", message);
        }

        private async Task SendFormattedEmail(string email, string subject, string htmlMessage)
        {
            try
            {
                var emailSettings = _configuration.GetSection("emailSettings");
                using var client = new SmtpClient();

                await client.ConnectAsync("smtp.gmail.com", 465, true);
                client.Authenticate(emailSettings["FromEmail"],
                    Environment.GetEnvironmentVariable("EmailPassword"));

                var emailMessage = new MimeMessage
                {
                    Subject = subject,
                    Body = new BodyBuilder { HtmlBody = htmlMessage }.ToMessageBody(),
                    From = { new MailboxAddress("Recommendy Team", emailSettings["FromEmail"]) },
                    To = { new MailboxAddress("User", email) }
                };

                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email: {ex.Message}");
                throw;
            }
        }




        }
}
