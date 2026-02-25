namespace FeedEngine.DDD.API.Modules.Identity.Contracts
{
    public sealed record RegisterUserResponse(
    Guid UserId,
    string Email,
    string Username,
    string AccessToken);
}
