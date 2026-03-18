using Identity.Application.Abstractions.Authentication;
using Identity.Application.Abstractions.Messaging;
using Identity.Contracts.Events;
using Identity.Domain;
using Identity.Domain.Events;
using Identity.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Identity.Application.EventHandlers;

public class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{

    private readonly ILogger<UserRegisteredDomainEventHandler> _logger;
    private readonly IOutboxService _outboxService;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    public UserRegisteredDomainEventHandler(
        IJwtProvider jwtProvider,
        IUserRepository userRepository,
        IOutboxService outboxService,
        ILogger<UserRegisteredDomainEventHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
        _jwtProvider = jwtProvider;
        _outboxService = outboxService;
    }

    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user is null)
            throw new DomainException("User not found for registration event.");

        var (token, expiresIn) = _jwtProvider.GenerateEmailVerificationToken(user);

        var notificationIntegrationEvent = new EmailVerificationRequestedIntegrationEvent(
            user.Id.Value,
            user.Email.Value,
            user.Username.Value,
            token,
            expiresIn,
            notification.OccurredOnUtc);

        var auditIntegrationEvent = new UserRegisteredIntegrationEvent(Guid.NewGuid(), notification.UserId.Value, "New User Registered", DateTime.UtcNow);

        await _outboxService.AddAsync(auditIntegrationEvent, cancellationToken);
        await _outboxService.AddAsync(notificationIntegrationEvent, cancellationToken);

        _logger.LogInformation("User registered: {UserId}, {Email}", user.Id.Value, user.Email.Value);


    }
}
