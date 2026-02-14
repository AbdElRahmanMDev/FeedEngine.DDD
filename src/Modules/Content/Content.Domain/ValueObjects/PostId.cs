namespace Content.Domain.ValueObjects
{
    public record PostId
    {
        public Guid Value { get; }
        private PostId(Guid value) => Value = value;

        public static PostId New() => Of(Guid.NewGuid());
        public static PostId Of(Guid value)
         => value == Guid.Empty
           ? throw new DomainException("PostId cannot be empty.")
           : new PostId(value);
    }
}
