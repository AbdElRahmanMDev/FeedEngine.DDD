namespace Notification.Application.Abstractions;

public interface IVerificationLinkFactory
{
    string Create(string token);
}


