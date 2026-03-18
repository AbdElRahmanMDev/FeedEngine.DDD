using Microsoft.Extensions.Options;
using Notification.Application.Abstractions;

namespace Notification.Infrastructure.Services
{
    internal class VerificationLinkFactory : IVerificationLinkFactory
    {
        private readonly VerificationOptions _options;

        public VerificationLinkFactory(IOptions<VerificationOptions> options)
        {
            _options = options.Value;
        }

        public string Create(string token)
        {
            var encodedToken = Uri.EscapeDataString(token);
            return $"{_options.BaseUrl}?token={encodedToken}";
        }
    }
}
