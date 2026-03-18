using MediatR;

namespace Identity.Contracts.Events;

public record EmailVerificationRequestedIntegrationEvent(Guid UserId, string Email, string Username, string token, int expiresIn, DateTime utcNow) : INotification;

