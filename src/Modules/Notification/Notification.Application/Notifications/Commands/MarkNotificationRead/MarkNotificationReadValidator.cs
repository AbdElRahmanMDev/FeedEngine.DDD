using FluentValidation;

namespace Notification.Application.Notifications.Commands.MarkNotificationRead
{
    public class MarkNotificationReadValidator : AbstractValidator<MarkNotificationReadCommand>
    {
        public MarkNotificationReadValidator()
        {
            RuleFor(x => x.NotificationId).NotEmpty();
        }
    }

}
