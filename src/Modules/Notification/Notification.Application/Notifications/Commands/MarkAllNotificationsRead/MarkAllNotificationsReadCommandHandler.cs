using BuildingBlocks.Application.Abstraction;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Notification.Domain.Models;
using Notification.Domain.ValueObjects;

namespace Notification.Application.Notifications.Commands.MarkAllNotificationsRead
{
    internal class MarkAllNotificationsReadCommandHandler : ICommandHandler<MarkAllNotificationsReadCommand, int>
    {
        private readonly INotificationRepository _repo;
        private readonly ICurrentUserService _currentUserService;
        public MarkAllNotificationsReadCommandHandler(INotificationRepository repo, ICurrentUserService currentUserService)
        {
            _repo = repo;
            _currentUserService = currentUserService;
        }
        public async Task<Result<int>> Handle(MarkAllNotificationsReadCommand request, CancellationToken ct)

        {
            var userId = new UserId(_currentUserService.UserId.Value);

            var updated = await _repo.MarkAllAsReadAsync(userId, DateTimeOffset.UtcNow, ct);


            await _repo.SaveChangesAsync(ct);

            return updated;
        }
    }
}
