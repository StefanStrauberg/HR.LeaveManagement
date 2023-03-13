using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HR.LeaveManagement.Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        readonly EmailSettings _emailSettings;
        
        public EmailSender(IOptions<EmailSettings> emailSettings)
            => _emailSettings = emailSettings.Value;

        public async Task SendEmail(EmailMessage email)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_emailSettings.FromName,
                                                     _emailSettings.FromAddress));
            emailMessage.To.Add(new MailboxAddress("",
                                                   email.To));
            emailMessage.Subject = email.Subject;

            emailMessage.Body = new TextPart("Plain")
            {
                Text = email.Body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.SmtpServerAddress,
                                          _emailSettings.ServerPort,
                                          false);
                await client.AuthenticateAsync(_emailSettings.FromAddress,
                                               _emailSettings.EmailPassword);
                await client.SendAsync(emailMessage);
 
                await client.DisconnectAsync(true);
            }
        }
    }
}