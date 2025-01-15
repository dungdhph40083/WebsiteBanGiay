using System.Net.Mail;
using System.Net;
using Application.Data.Models;
using Microsoft.Extensions.Options;

namespace Application.API.Service
{
    public class EmailService : IEmailService
    {
        private readonly MailSetting _mailSetting;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IOptions<MailSetting> mailSetting, ILogger<EmailService> logger)
        {
            _mailSetting = mailSetting.Value;
            _logger = logger;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine();
            Console.WriteLine("Email Confirmation Message");
            Console.WriteLine("--------------------------");
            Console.WriteLine($"TO: {email}");
            Console.WriteLine($"SUBJECT: {subject}");
            Console.WriteLine($"CONTENTS: {htmlMessage}");
            Console.WriteLine();

            var fromAddress = new MailAddress(_mailSetting.Mail, _mailSetting.DisplayName);
            var toAddress = new MailAddress(email);

            var smtp = new SmtpClient
            {
                Host = _mailSetting.Host,
                Port = _mailSetting.Port,
                EnableSsl = _mailSetting.EnableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, _mailSetting.Password),
                Timeout = _mailSetting.Timeout
            };

            var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = htmlMessage
            };
            // Turn on or off the email sending
           await smtp.SendMailAsync(mailMessage);
        }
    }
}
