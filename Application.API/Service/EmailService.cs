using System.Net.Mail;
using System.Net;

namespace Application.API.Service
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MailMessage();
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var client = new SmtpClient("smtp.your-email-provider.com"))
            {
                client.Credentials = new NetworkCredential("your-email@example.com", "your-email-password");
                client.Port = 587;
                await client.SendMailAsync(message);
            }
        }
    }
}
