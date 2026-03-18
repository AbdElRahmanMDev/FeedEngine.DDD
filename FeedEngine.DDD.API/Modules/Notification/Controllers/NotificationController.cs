using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Notification.Queries.GetNotifications;

namespace FeedEngine.DDD.API.Modules.Notification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly ISender _sender;
        public NotificationController(ISender sender)
        {
            _sender = sender;
        }
        [HttpGet("notifications")]
        public async Task<IResult> GetMyNotifications(
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetMyNotificationsQuery(), cancellationToken);
            return Results.Ok(result);
        }
    }
}
