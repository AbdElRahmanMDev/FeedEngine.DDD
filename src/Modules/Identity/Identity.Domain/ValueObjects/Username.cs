using Identity.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Identity.Domain.ValueObjects;

public sealed record Username
{
    public string Value { get; }

    private Username(string value) => Value = value;

    // Letters/digits/._ only, 3..20 chars, no spaces
    private static readonly Regex Allowed =
        new(@"^[a-zA-Z0-9._]{3,20}$", RegexOptions.Compiled);

    public static Username Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new DomainException("Username is required.");

        var trimmed = input.Trim();

        // Normalize: lower-case (common for usernames)
        var normalized = trimmed.ToLowerInvariant();

        if (!Allowed.IsMatch(normalized))
            throw new DomainException("Username must be 3-20 chars and contain only letters, digits, '.' or '_'.");

        return new Username(normalized);
    }

    public override string ToString() => Value;
}
