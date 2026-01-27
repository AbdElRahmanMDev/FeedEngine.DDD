using Identity.Domain.Exceptions;
using System.Net.Mail;

namespace Identity.Domain.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; }

        private Email(string value) => Value = value;

        public static Email Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new DomainException("Email is required.");

            var trimmed = input.Trim();

            // Basic validation using MailAddress (good enough for MVP)
            try
            {
                var addr = new MailAddress(trimmed);
                // Normalize: lower-case
                return new Email(addr.Address.ToLowerInvariant());
            }
            catch
            {
                throw new DomainException("Email is invalid.");
            }
        }

        public override string ToString() => Value;
    }
}
