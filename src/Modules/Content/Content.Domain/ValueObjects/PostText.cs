
namespace Content.Domain.ValueObjects;

public sealed record PostText
{
    public const int MaxLength = 5000;

    public string Value { get; }

    private PostText(string value) => Value = value;

    public static PostText Create(string value)
    {
        value = (value ?? "").Trim();

        if (value.Length == 0)
            throw new ArgumentException("Post text is required.");

        if (value.Length > MaxLength)
            throw new ArgumentException($"Post text cannot exceed {MaxLength} characters.");

        return new PostText(value);
    }
    public override string ToString() => Value;
}

