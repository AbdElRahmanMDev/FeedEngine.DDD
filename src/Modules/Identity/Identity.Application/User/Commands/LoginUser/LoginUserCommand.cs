using BuildingBlocks.Application.Messaging;
using Identity.Application.User.DTOs;

namespace Identity.Application.User.Commands.LoginUser;

public record LoginUserCommand(string Email, string Password) : ICommand<LoginUserDTO>;

