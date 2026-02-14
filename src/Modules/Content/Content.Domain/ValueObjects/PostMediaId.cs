
namespace Content.Domain.ValueObjects;

public record PostMediaId
{
    public Guid Value { get; }
    private PostMediaId(Guid value) => Value = value;

    public static PostMediaId New() => Of(Guid.NewGuid());
    public static PostMediaId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("PostMediaId cannot be empty.");
        }

        return new PostMediaId(value);
    }
}
