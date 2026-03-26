namespace Notification.Application.Notifications.Commands.CreateFollowerNotifications
{
    //public class CreateFollowerNotificationsCommandHandler : IQueryHandler<CreateFollowerNotificationsCommand, Unit>
    //{
    //    //private readonly ISocialModule _socialModule;
    //    //private readonly INotificationRepository _notificationRepository;
    //    //public CreateFollowerNotificationsCommandHandler(ISocialModule socialModule, INotificationRepository notificationRepository)
    //    //{
    //    //    _socialModule = socialModule;
    //    //    _notificationRepository = notificationRepository;
    //    //}
    //    //public async Task<Result<Unit>> Handle(CreateFollowerNotificationsCommand request, CancellationToken cancellationToken)
    //    //{

    //    //    var followers = (await _socialModule.GetFollowersAsync(request.UserId.Value, cancellationToken))
    //    //            .Select(x => UserId.Create(x))
    //    //            //.Select(x=> NotificationItem.Create(request.UserId, x, Domain.Models.enums.NotificationType.Mention, NotificationMessage.Create("Post Created", "new Post Created"), new DeepLink("sda")))
    //    //            .ToList();


    //    //    if (followers.Count == 0)
    //    //        return Unit.Value;


    //    //    //var notifications = followers.Select(x => NotificationItem.Create(request.UserId, x, Domain.Models.enums.NotificationType.Mention, NotificationMessage.Create("Post Created", "new Post Created"), new DeepLink("sda")))
    //    //    //  .ToList();

    //    //    //await _notificationRepository.AddRange(notifications, cancellationToken);
    //    //    //await _notificationRepository.SaveChangesAsync(cancellationToken);



    //        return Result.Success(Unit.Value);


    //    }
    //}
}
