using Identity.Contracts.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Application.Abstractions;

namespace Notification.Application.Handlers;

public class EmailVerificationRequestedIntegrationEventHandler : INotificationHandler<EmailVerificationRequestedIntegrationEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly IVerificationLinkFactory _verificationLinkFactory;
    private readonly ILogger<EmailVerificationRequestedIntegrationEventHandler> _logger;

    public EmailVerificationRequestedIntegrationEventHandler(
        IEmailSender emailSender,
        IVerificationLinkFactory verificationLinkFactory,
        ILogger<EmailVerificationRequestedIntegrationEventHandler> logger)
    {
        _emailSender = emailSender;
        _verificationLinkFactory = verificationLinkFactory;
        _logger = logger;
    }

    public async Task Handle(EmailVerificationRequestedIntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        var verificationLink = _verificationLinkFactory.Create(integrationEvent.token);

        var subject = "Verify your email";

        var body =
            $"Hello {integrationEvent.Username},\n\n" +
            $"Please verify your email by clicking the link below:\n" +
            $"{verificationLink}\n\n" +
            $"This link expires in {integrationEvent.expiresIn / 60} minutes.\n\n" +
            $"If you did not create this account, you can ignore this email.";

        await _emailSender.SendAsync(
            new EmailMessage(integrationEvent.Email, subject, body),
            cancellationToken);

        _logger.LogInformation(
            "Verification email sent to {Email} for user {UserId}",
            integrationEvent.Email,
            integrationEvent.UserId);
    }
}

