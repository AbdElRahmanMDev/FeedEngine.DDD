namespace Notification.Domain.ValueObjects
{
    public record UserId(Guid Value)
    {

        public static UserId Create(Guid value) => new(value);

        public static UserId System()
            => new(Guid.Parse("00000000-0000-0000-0000-000000000001"));
        public override string ToString() => Value.ToString();
    }
}
