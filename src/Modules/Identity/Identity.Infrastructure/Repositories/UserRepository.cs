using Identity.Domain;
using Identity.Domain.Models;
using Identity.Domain.ValueObjects;
using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UsersDbContext _usersDbContext;
    public UserRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }
    public async Task AddAsync(User user, CancellationToken ct = default) => await _usersDbContext.Users.AddAsync(user, ct);

    public Task<bool> EmailExistsAsync(Email email, CancellationToken ct = default)
    {
        return _usersDbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == email, ct);
    }
    public async Task<bool> UsernameExistsAsync(Username username, CancellationToken ct = default)
    {
        return await _usersDbContext.Users.AnyAsync(x => x.Username == username, ct);
    }

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken ct = default) =>
        await _usersDbContext.Users.Where(x => x.Id == id).SingleOrDefaultAsync();


    public async Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default) =>
        await _usersDbContext.Users.Where(x => x.Email == email).SingleOrDefaultAsync();
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _usersDbContext.SaveChangesAsync(ct);


}
