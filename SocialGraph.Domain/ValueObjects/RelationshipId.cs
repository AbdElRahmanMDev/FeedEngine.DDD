namespace SocialGraph.Domain.ValueObjects
{
    public record RelationshipId(Guid Value)
    {
        public static RelationshipId Create(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("RelationshipId cannot be empty.");

            return new RelationshipId(value);
        }

        public static RelationshipId New() => new(Guid.NewGuid());

        public override string ToString() => Value.ToString();
    }

}
