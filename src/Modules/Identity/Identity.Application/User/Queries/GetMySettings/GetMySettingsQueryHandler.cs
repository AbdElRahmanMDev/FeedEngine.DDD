using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Identity.Application.Abstractions.Authentication;
using Identity.Application.User.DTOs;
using Identity.Domain;

namespace Identity.Application.User.Queries.GetMySettings
{
    internal class GetMySettingsQueryHandler : IQueryHandler<GetMySettingsQuery, UserSettingsDto>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IUserSettingsRepository _repo;

        public GetMySettingsQueryHandler(ICurrentUserService currentUser, IUserSettingsRepository repo)
        {
            _currentUser = currentUser;
            _repo = repo;
        }

        public async Task<Result<UserSettingsDto>> Handle(GetMySettingsQuery request, CancellationToken ct)
        {
            var settings = await _repo.GetByUserIdAsync(_currentUser.UserId, ct)
                          ?? throw new InvalidOperationException("User settings not found.");

            return new UserSettingsDto(
                settings.Language,
                settings.TimeZone,
                settings.EmailNotificationsEnabled,
                settings.PushNotificationsEnabled,
                settings.IsProfilePrivate
            );
        }


    }
}
