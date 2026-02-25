using BuildingBlocks.Domain.Abstraction;
using SocialGraph.Domain.ValueObjects;

namespace SocialGraph.Domain.Events;


public record UserBlockedDomainEvent(UserId UserId, UserId BlockedUserId) : IDomainEvent;
