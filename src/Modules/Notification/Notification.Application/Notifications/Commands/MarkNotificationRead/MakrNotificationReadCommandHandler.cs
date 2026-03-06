using BuildingBlocks.Application.Abstraction;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using MediatR;
using Notification.Domain.Models;
using Notification.Domain.ValueObjects;

namespace Notification.Application.Notifications.Commands.MarkNotificationRead
{
    public class MakrNotificationReadCommandHandler : ICommandHandler<MarkNotificationReadCommand, Unit>
    {
        private readonly INotificationRepository _repo;
        private readonly ICurrentUserService _currentUserService;
        public MakrNotificationReadCommandHandler(INotificationRepository repository, ICurrentUserService currentUserService)
        {
            _repo = repository;
            _currentUserService = currentUserService;
        }
        public async Task<Result<Unit>> Handle(MarkNotificationReadCommand request, CancellationToken ct)
        {
            var userId = new UserId(_currentUserService!.UserId!.Value);
            var notificationId = new NotificationId(request.NotificationId);

            var notification = await _repo.GetForUserByIdAsync(userId, notificationId, ct)
                ?? throw new KeyNotFoundException("Notification not found for this user.");

            notification.MarkRead();
            await _repo.SaveChangesAsync(ct);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
