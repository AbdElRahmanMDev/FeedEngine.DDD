using BuildingBlocks.Application.Messaging;
using Identity.Application.User.DTOs;

namespace Identity.Application.User.Commands.RegisterUser;

public record RegisterUserCommand(
    string Email,
    string Username,
    string Password
    ) : ICommand<RegisterUserResult>;

