namespace SocialGraph.Domain.ValueObjects
{
    public record UserId(Guid Value)
    {
        public static UserId Create(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.");

            return new UserId(value);
        }

        public override string ToString() => Value.ToString();
    }
}
