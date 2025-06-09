using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using CORE.Models;
using BL.Services.Abstracts;
namespace BL.Services.Concretes
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
                EnableSsl = true, 
                UseDefaultCredentials = false
            };

            var message = new MailMessage(_emailSettings.SenderEmail, to, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(message);
        }
    }
}
