using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using MediatR;
using Notification.Application.Abstractions;
using Notification.Domain.Models;
using Notification.Domain.ValueObjects;

namespace Notification.Application.Notifications.Commands.SendWelcomeNotification
{
    public class SendWelcomeNotificationCommandHandler : ICommandHandler<SendWelcomeNotificationCommand, Unit>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IEmailSender _emailSender;
        public SendWelcomeNotificationCommandHandler(INotificationRepository notificationRepository, IEmailSender emailSender)
        {
            _notificationRepository = notificationRepository;
            _emailSender = emailSender;
        }
        public async Task<Result<Unit>> Handle(SendWelcomeNotificationCommand request, CancellationToken cancellationToken)
        {

            var recipientUserId = UserId.Create(request.UserId);

            var notification = await _notificationRepository.GetWelcomeByRecipientIdAsync(
                recipientUserId,
                cancellationToken);

            if (notification is null)
            {
                notification = NotificationItem.CreateWelcome(recipientUserId);

                await _notificationRepository.AddAsync(notification, cancellationToken);
                await _notificationRepository.SaveChangesAsync(cancellationToken);
            }

            if (notification.IsEmailSent)
                return Result<Unit>.Success(Unit.Value);

            var emailMessage = WelcomeEmailFactory.Create(
                request.Email,
                request.UserName);

            await _emailSender.SendAsync(emailMessage, cancellationToken);

            notification.MarkEmailSent();

            await _notificationRepository.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }

}
