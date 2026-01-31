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

    public Task<bool> EmailExistsAsync(string email, CancellationToken ct = default) =>
     _usersDbContext.Users
         .AsNoTracking()
         .AnyAsync(u => u.Email.Value == email, ct);
    public async Task<bool> UsernameExistsAsync(string username, CancellationToken ct = default) => await _usersDbContext.Users.AnyAsync(x => x.Username.Value == username, ct);

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken ct = default) => await _usersDbContext.Users.Where(x => x.Id == id).SingleOrDefaultAsync();


    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _usersDbContext.SaveChangesAsync(ct);
}
