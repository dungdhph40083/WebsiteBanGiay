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
            Console.WriteLine("To: " + string.Join(", ", message.To));
            Console.WriteLine("Subject: " + message.Subject);
            Console.WriteLine("Body: " + message.Body);
            Console.WriteLine("Is Body Html: " + message.IsBodyHtml);
        }
    }
}
