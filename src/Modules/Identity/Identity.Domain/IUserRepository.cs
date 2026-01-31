using Identity.Domain.Models;
using Identity.Domain.ValueObjects;

namespace Identity.Domain
{
    public interface IUserRepository
    {

        Task AddAsync(User user, CancellationToken ct = default);
        Task<User?> GetByIdAsync(UserId id, CancellationToken ct = default);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task<bool> UsernameExistsAsync(string username, CancellationToken ct = default);
        Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);

    }
}
