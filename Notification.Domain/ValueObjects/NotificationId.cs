namespace Notification.Domain.ValueObjects;

public sealed record NotificationId(Guid Value)
{
    public static NotificationId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("NotificationId cannot be empty.");

        return new NotificationId(value);
    }
};

