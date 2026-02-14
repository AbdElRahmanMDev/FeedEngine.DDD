using Identity.Domain.Models;

namespace Identity.Domain
{
    public interface IUserSettingsRepository
    {
        Task<UserSettings?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
        void Add(UserSettings settings);
    }
}
