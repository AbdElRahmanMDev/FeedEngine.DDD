using Identity.Domain;
using Identity.Domain.ValueObjects;
using MediatR;

namespace Identity.Application.User.Commands.UpdateMySettings
{
    public class UpdateMySettingsCommandHandler : ICommandHandler<UpdateMySettingsCommand, Unit>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        public UpdateMySettingsCommandHandler(IUserRepository userRepository, ICurrentUserService currentUser)
        {
            _userRepository = userRepository;
            _currentUserService = currentUser;
        }

        public async Task<Result<Unit>> Handle(UpdateMySettingsCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByIdAsync(UserId.Of(_currentUserService.UserId!.Value), cancellationToken);

            if (user is null)
                return Result.Failure<Unit>(new Error("User not found", "User not found"));
            var current = user.Settings;

            var theme = request.Theme ?? current.Theme;

            var language = request.Language ?? current.Language;
            var notif = request.NotificationsEnabled ?? current.NotificationsEnabled;
            var privacy = request.PrivacyLevel ?? current.PrivacyLevel;

            user.ChangeSettings(
                notif,
                privacy,
                DateTime.UtcNow,
                theme,
                request.Language!
            ); await _userRepository.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
