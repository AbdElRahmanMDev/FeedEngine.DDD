using Identity.Domain;
using Identity.Domain.Models;
using Identity.Domain.ValueObjects;
using Identity.Infrastructure.Database;

namespace Identity.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UsersDbContext _usersDbContext;
    public UserRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }
    public async Task AddAsync(User user, CancellationToken ct = default)
    {
        await _usersDbContext.Users.AddAsync(user, ct);
    }

    public async Task<bool> EmailExistsAsync(Email email, CancellationToken ct = default)
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

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _usersDbContext.SaveChangesAsync(ct);
}
