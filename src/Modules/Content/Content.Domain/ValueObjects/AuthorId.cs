

namespace Content.Domain.ValueObjects
{
    public readonly record struct AuthorId(Guid Value)
    {
        public static AuthorId Of(Guid value)
            => value == Guid.Empty ? throw new DomainException("AuthorId cannot be empty.")
                                   : new AuthorId(value);
    }
}
