namespace Identity.Contracts.Abstractions
{
    public interface IIdentityUserValidator
    {
        Task<UserValidationResult> ValidateUserAsync(Guid follower, Guid followed);
    }

    public record UserValidationResult(
         bool FollowerExists,
        bool FollowerActive,
        bool FollowerDeleted,
        bool FollowedExists,
        bool FollowedActive,
        bool FollowedDeleted

        );

}
