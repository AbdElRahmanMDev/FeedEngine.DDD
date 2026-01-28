using Identity.Domain;
using Identity.Domain.Models;
using Identity.Domain.ValueObjects;

namespace Identity.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public Task AddAsync(User user, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EmailExistsAsync(Email email, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(UserId id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UsernameExistsAsync(Username username, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
