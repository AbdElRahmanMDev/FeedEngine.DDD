namespace FeedEngine.DDD.API.Modules.Identity.Contracts;

public sealed record ChangePasswordRequest(
   string CurrentPassword,
   string NewPassword);
