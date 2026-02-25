using Identity.Domain.Exceptions;
namespace Identity.Domain.ValueObjects;

public sealed record UserId
{
    public Guid Value { get; }
    private UserId(Guid value) => Value = value;

    public static UserId New() => Of(Guid.NewGuid());
    public static UserId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("OrderId cannot be empty.");
        }

        return new UserId(value);
    }
}
