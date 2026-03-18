using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialGraph.Application.Relationships.Commands.FollowUser;
using SocialGraph.Application.Relationships.Queries.GetFollowers;

namespace FeedEngine.DDD.API.Modules.SocialGraph.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SocialGraphController : ControllerBase
    {
        private readonly ISender _sender;

        public SocialGraphController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpPost("{userId:guid}/follow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{userId:guid}")]
        public async Task<IActionResult> Follow(Guid userId, CancellationToken cancellationToken)
        {
            var command = new FollowUserCommand(userId);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpGet("followers")]
        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFollowers(CancellationToken cancellationToken)
        {
            var query = new GetFollowersQuery();

            var result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var response = result.Value
                .Select(x => x.Value)
                .ToList();

            return Ok(response);
        }


    }
}
