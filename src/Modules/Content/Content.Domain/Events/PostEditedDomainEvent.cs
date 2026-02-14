using BuildingBlocks.Domain.Abstraction;
using Content.Domain.ValueObjects;
namespace Content.Domain.Events;

public sealed record PostEditedDomainEvent(PostId PostId, AuthorId AuthorId, DateTime OccurredAtUtc) : IDomainEvent;
