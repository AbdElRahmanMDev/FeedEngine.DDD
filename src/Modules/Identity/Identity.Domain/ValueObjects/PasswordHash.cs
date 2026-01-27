using Identity.Domain.Exceptions;

namespace Identity.Domain.ValueObjects
{
    public sealed record PasswordHash
    {
        public string Value { get; }

        private PasswordHash(string value) => Value = value;

        public static PasswordHash FromHash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new DomainException("Password hash is required.");

            if (hash.Length < 20) // simple guard to avoid obvious mistakes (plain password)
                throw new DomainException("Password hash looks invalid.");

            return new PasswordHash(hash);
        }

        public override string ToString() => Value;
    }
}
