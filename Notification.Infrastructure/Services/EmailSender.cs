using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Notification.Application.Abstractions;

namespace Notification.Infrastructure.Services
{
    public class EmailSender(IOptions<MailSettings> mailSetting) : IEmailSender
    {
        private readonly MailSettings _mailSettings = mailSetting.Value;

        public async Task SendAsync(EmailMessage msg, CancellationToken cancellationToken)
        {
            var message = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail),
                Subject = msg.Subject
            };

            message.To.Add(MailboxAddress.Parse(msg.To));

            message.Body = new TextPart("plain")
            {
                Text = msg.Body
            };

            using var smtp = new SmtpClient();

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);

        }
    }
}
