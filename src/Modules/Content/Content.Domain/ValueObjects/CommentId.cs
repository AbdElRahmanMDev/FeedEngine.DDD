namespace Content.Domain.ValueObjects
{
    public readonly record struct CommentId(Guid Value)
    {
        public static CommentId New() => new(Guid.NewGuid());

        public static CommentId Of(Guid value)
            => value == Guid.Empty
                ? throw new DomainException("CommentId cannot be empty.")
                : new CommentId(value);

        public override string ToString() => Value.ToString();
    }
}
