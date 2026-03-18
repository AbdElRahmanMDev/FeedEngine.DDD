

using BuildingBlocks.Application.Abstraction;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Identity.Contracts.Abstractions;
using MediatR;
using SocialGraph.Domain;
using SocialGraph.Domain.Exceptions;
using SocialGraph.Domain.Models;
using SocialGraph.Domain.ValueObjects;

namespace SocialGraph.Application.Relationships.Commands.FollowUser
{
    public class FollowUserCommandHandler : ICommandHandler<FollowUserCommand, Unit>
    {
        private readonly IFollowRelationshipRepository _followRelationshipRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityUserValidator _identityUserValidator;

        public FollowUserCommandHandler(IFollowRelationshipRepository followRelationshipRepository,
            ICurrentUserService currentUserService,
            IIdentityUserValidator identityUserValidator)
        {
            _followRelationshipRepository = followRelationshipRepository;
            _currentUserService = currentUserService;
            _identityUserValidator = identityUserValidator;
        }
        public async Task<Result<Unit>> Handle(FollowUserCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("User is not authenticated");

            var validation = await _identityUserValidator.ValidateUserAsync(request.UserId, currentUserId);

            if (!validation.FollowerExists || validation.FollowerDeleted || !validation.FollowerActive)
                throw new BusinessRuleValidationException("Follower user is invalid.");

            if (!validation.FollowedExists || validation.FollowedDeleted || !validation.FollowedActive)
                throw new BusinessRuleValidationException("Target user is invalid.");

            var isFollowing = await _followRelationshipRepository.IsFollowingAsync(UserId.Create(currentUserId), UserId.Create(request.UserId));
            if (isFollowing)
                throw new BusinessRuleValidationException("User is already following.");

            var followRelationship = FollowRelationship.Create(UserId.Create(currentUserId), UserId.Create(request.UserId));

            await _followRelationshipRepository.AddAsync(followRelationship);
            await _followRelationshipRepository.SaveChangesAsync();

            return Result.Success(Unit.Value);
        }
    }
}
