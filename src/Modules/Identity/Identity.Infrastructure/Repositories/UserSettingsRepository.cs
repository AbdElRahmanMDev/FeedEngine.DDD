using Identity.Domain;
using Identity.Domain.Models;
using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories
{
    internal class UserSettingsRepository : IUserSettingsRepository
    {
        private readonly UsersDbContext _db;
        public UserSettingsRepository(UsersDbContext db) => _db = db;

        public void Add(UserSettings settings) => _db.UserSettings.Add(settings);



        public async Task<UserSettings?> GetByUserIdAsync(Guid userId, CancellationToken ct = default) => await _db.UserSettings.FirstOrDefaultAsync(x => x.UserId == userId, ct);

    }
}
