using BuildingBlocks.Domain.Abstraction;
using FeedEngine.DDD.API.Modules.Identity.Contracts;
using Identity.Application.User.Commands.ChangeEmail;
using Identity.Application.User.Commands.ChangePassword;
using Identity.Application.User.Commands.RegisterUser;
using Identity.Application.User.DTOs;
using Identity.Application.User.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FeedEngine.DDD.API.Modules.Identity.Controllers
{
    [Route("api/identity/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        // POST api/identity/users/register
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterUserResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken ct)
        {
            var command = new RegisterUserCommand(request.Email, request.Username, request.Password);

            Result<RegisterUserResult> result = await _mediator.Send(command, ct);

            if (result.IsFailure)
                return ToProblem(result.Error);

            // Optional: point location to GET by id
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Value.UserId },
                result.Value
            );
        }

        // GET api/identity/users/{id}
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var query = new GetUserByIdQuery(id);

            Result<UserDto> result = await _mediator.Send(query, ct);

            if (result.IsFailure)
                return ToProblem(result.Error);

            return Ok(result.Value);
        }

        // PUT api/identity/users/{id}/email
        [HttpPut("{id:guid}/email")]
        [ProducesResponseType(typeof(ChangeEmailResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeEmail([FromRoute] Guid id, [FromBody] ChangeEmailRequest request, CancellationToken ct)
        {
            var command = new ChangeEmailCommand(id, request.NewEmail);

            Result<ChangeEmailResult> result = await _mediator.Send(command, ct);

            if (result.IsFailure)
                return ToProblem(result.Error);

            return Ok(result.Value);
        }

        // PUT api/identity/users/{id}/password
        [HttpPut("{id:guid}/password")]
        [ProducesResponseType(typeof(ChangePasswordResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromRoute] Guid id, [FromBody] ChangePasswordRequest request, CancellationToken ct)
        {
            // Mapping plaintext password -> command property (named NewPasswordHash in your app layer)
            var command = new ChangePasswordCommand(id, request.NewPassword);

            Result<ChangePasswordResult> result = await _mediator.Send(command, ct);

            if (result.IsFailure)
                return ToProblem(result.Error);

            return Ok(result.Value);
        }

        private IActionResult ToProblem(Error error)
        {
            // Map known errors to HTTP codes
            var statusCode =
                error.Code == "User.NotFound" ? StatusCodes.Status404NotFound :
                StatusCodes.Status400BadRequest;

            return Problem(
                title: error.Code,
                detail: error.Message,
                statusCode: statusCode
            );
        }

    }


}
