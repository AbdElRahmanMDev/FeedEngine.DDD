using BuildingBlocks.Domain.Abstraction;
using FeedEngine.DDD.API.Modules.Identity.Contracts;
using Identity.Application.User.Commands.ChangeEmail;
using Identity.Application.User.Commands.ChangePassword;
using Identity.Application.User.Commands.LoginUser;
using Identity.Application.User.Commands.RegisterUser;
using Identity.Application.User.DTOs;
using Identity.Application.User.Queries.GetUserById;
using Identity.Application.User.Queries.GetUserSettings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> GetById(CancellationToken ct)
        {
            var query = new GetUserByIdQuery();

            Result<UserDto> result = await _mediator.Send(query, ct);

            if (result.IsFailure)
                return ToProblem(result.Error);

            return Ok(result.Value);
        }

        // PUT api/identity/users/{id}/email
        [HttpPut("email")]
        [ProducesResponseType(typeof(ChangeEmailResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request, CancellationToken ct)
        {
            var command = new ChangeEmailCommand(request.NewEmail);

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
            var command = new ChangePasswordCommand(request.NewPassword);

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



        //[HttpPut("me/settings")]
        //public async Task<IActionResult> UpdateMySettings(UpdateMySettingsCommand command, CancellationToken ct)
        //{
        //    await _mediator.Send(command, ct);
        //    return NoContent();
        //}

        [HttpGet("me/settings")]
        [ProducesResponseType(typeof(UserSettingsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> GetMySettings(CancellationToken ct)
        {
            var query = new GetUserSettingsQuery();

            Result<UserSettingsDto> result = await _mediator.Send(query, ct);

            if (result.IsFailure)
                return ToProblem(result.Error);

            return Ok(result.Value);
        }
        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginUserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken ct)
        {
            var command = new LoginUserCommand(
                request.Email,
                request.Password);

            Result<LoginUserDTO> result = await _mediator.Send(command, ct);

            if (result.IsFailure)
                return ToProblem(result.Error);

            return Ok(result.Value);
        }


        //[HttpPut("me/settings")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        //[Authorize]
        //public async Task<IActionResult> UpdateMySettings([FromBody] UpdateMySettingsRequest request, CancellationToken ct)
        //{
        //    var command = new UpdateMySettingsCommand(
        //        request.Language,
        //        request.Theme,
        //        request.EmailNotificationsEnabled,
        //        request.PrivacyLevel,
        //        request.IsProfilePrivate
        //    );

        //    Result<Unit> result = await _mediator.Send(command, ct);

        //    if (result.IsFailure)
        //        return ToProblem(result.Error);

        //    return NoContent();
        //}

    }


}
