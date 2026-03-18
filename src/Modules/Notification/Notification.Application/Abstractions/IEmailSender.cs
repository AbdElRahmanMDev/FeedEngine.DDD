namespace Notification.Application.Abstractions
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message, CancellationToken cancellationToken);
    }
}
