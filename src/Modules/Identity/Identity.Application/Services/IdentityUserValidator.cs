using Identity.Contracts.Abstractions;
using Identity.Domain;
using Identity.Domain.Models.enums;
using Identity.Domain.ValueObjects;

namespace Identity.Application.Services
{
    public class IdentityUserValidator : IIdentityUserValidator
    {
        private readonly IUserRepository _userRepository;
        public IdentityUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserValidationResult> ValidateUserAsync(Guid followerId, Guid followedId)
        {
            var followerTask = _userRepository.GetByIdAsync(UserId.Of(followerId));
            var followedTask = _userRepository.GetByIdAsync(UserId.Of(followedId));

            await Task.WhenAll(followerTask, followedTask);

            var follower = followerTask.Result;
            var followed = followedTask.Result;

            bool followerExists = follower is not null;
            bool followedExists = followed is not null;

            bool followerIsActive = followerExists && follower.Status == AccountStatus.Active;
            bool followedIsActive = followedExists && followed.Status == AccountStatus.Active;

            bool followerIsDeleted = followerExists && follower.IsDeleted;
            bool followedIsDeleted = followedExists && followed.IsDeleted;

            return new UserValidationResult(
                followerIsActive,
                followerExists,
                followedExists,
                followedIsDeleted,
                followerIsDeleted,
                followedIsActive
            );

        }
    }
}
