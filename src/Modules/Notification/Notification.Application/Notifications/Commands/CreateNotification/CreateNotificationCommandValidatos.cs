using FluentValidation;

namespace Notification.Application.Notifications.Commands.CreateNotification
{
    internal class CreateNotificationCommandValidatos : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidatos()
        {
            RuleFor(x => x.RecipientUserId).NotEmpty();

            RuleFor(x => x.Type).IsInEnum();

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(120);

            RuleFor(x => x.Body)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.DeepLink)
                .NotEmpty()
                .MaximumLength(2048);
        }
    }

}
