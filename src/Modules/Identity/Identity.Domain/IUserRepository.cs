using Identity.Domain.Models;
using Identity.Domain.ValueObjects;

namespace Identity.Domain
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(Email email, CancellationToken ct = default);
        Task<bool> UsernameExistsAsync(Username username, CancellationToken ct = default);

        Task AddAsync(User user, CancellationToken ct = default);
        Task<User?> GetByIdAsync(UserId id, CancellationToken ct = default);
    }
}
