namespace FeedEngine.DDD.API.Modules.Identity.Contracts
{
    public sealed record LoginUserRequest(
     string Email,
     string Password);
}
