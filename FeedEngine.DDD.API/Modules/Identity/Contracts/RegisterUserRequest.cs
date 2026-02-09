namespace FeedEngine.DDD.API.Modules.Identity.Contracts;

public sealed record RegisterUserRequest(
   string Email,
   string Username,
   string Password);
