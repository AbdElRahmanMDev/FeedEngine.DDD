using BuildingBlocks.Application.Messaging;
using Identity.Application.Abstractions.Authentication;
using Identity.Domain;
using MediatR;

namespace Identity.Application.User.Commands.UpdateMySettings
{
    public class UpdateMySettingsCommandHandler : ICommandHandler<UpdateMySettingsCommand, Unit>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IUserSettingsRepository _repo;
        private readonly IUnitOfWork _uow; // or your DbContext

        public UpdateMySettingsCommandHandler(
            ICurrentUserService currentUser,
            IUserSettingsRepository repo,
            IUnitOfWork uow)
        {
            _currentUser = currentUser;
            _repo = repo;
            _uow = uow;
        }

        public async Task Handle(UpdateMySettingsCommand request, CancellationToken ct)
        {
            var settings = await _repo.GetByUserIdAsync(_currentUser.UserId, ct)
                          ?? throw new InvalidOperationException("User settings not found.");

            settings.Update(
                request,
                request.TimeZone,
                request.EmailNotificationsEnabled,
                request.PushNotificationsEnabled,
                request.IsProfilePrivate);

            await _uow.SaveChangesAsync(ct);
        }

    }
}
