using BuildingBlocks.Application.Abstraction;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Notification.Domain.Models;
using Notification.Domain.ValueObjects;


namespace Notification.Application.Notifications.Commands.CreateNotification
{
    public class CreateNotificationCommandHandler : ICommandHandler<CreateNotificationCommand, Guid>
    {
        private readonly INotificationRepository _repo;
        private readonly ICurrentUserService _currentUserService;
        public CreateNotificationCommandHandler(INotificationRepository repo, ICurrentUserService currentUserService)
        {
            _repo = repo;
            _currentUserService = currentUserService;
        }

        public async Task<Result<Guid>> Handle(CreateNotificationCommand request, CancellationToken ct)
        {
            var notification = NotificationItem.Create(new UserId(_currentUserService!.UserId!.Value),
            request.Type,
            new NotificationMessage(request.Title, request.Body),
            new DeepLink(request.DeepLink));

            await _repo.AddAsync(notification, ct);
            await _repo.SaveChangesAsync(ct);

            return notification.Id.Value;
        }
    }
}
