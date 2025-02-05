using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace Service
{
    public class EmailsService : IEmailsService
    {
        
        private readonly IConfiguration _configuration;
      
        public EmailsService(IConfiguration configuration)
        {
               _configuration = configuration;
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
        
    }
}
