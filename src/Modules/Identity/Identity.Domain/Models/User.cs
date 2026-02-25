using BuildingBlocks.Domain.Abstraction;
using Identity.Domain.Events;
using Identity.Domain.Exceptions;
using Identity.Domain.Models.enums;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Models
{
    public class User : Aggregate<UserId>
    {
        private User()
        {
        }

        public UserId Id { get; private set; }
        public Email Email { get; private set; }
        public Username Username { get; private set; }
        public PasswordHash PasswordHash { get; private set; }

        public UserSettings Settings { get; private set; }
        public bool EmailVerified { get; private set; }
        public AccountStatus Status { get; private set; }

        private User(UserId id, Email email, Username username, PasswordHash passwordHash, DateTime nowUtc)
        {
            Id = id;
            Email = email;
            Username = username;
            PasswordHash = passwordHash;
            Settings = UserSettings.Default();
            EmailVerified = false;
            Status = AccountStatus.Active;

            CreatedAt = nowUtc;
        }

        public static User Register(string email, string username, string passwordHash, DateTime nowUtc)
        {
            var user = new User(
                UserId.New(),
                Email.Create(email),
                Username.Create(username),
                PasswordHash.FromHash(passwordHash),
                nowUtc);
            user.Settings = UserSettings.Default();

            user.Raise(new UserRegisteredDomainEvent(user.Id, user.Email, user.Username, nowUtc));
            return user;
        }
        public void ChangeEmail(string newEmail, DateTime nowUtc)
        {
            EnsureActive();

            var next = Email.Create(newEmail);
            if (next == Email) return;

            var old = Email;

            Email = next;
            EmailVerified = false; // new email requires verification again
            Touch(nowUtc);

            Raise(new UserEmailChangedDomainEvent(Id, old, next, nowUtc));
        }
        public void ChangeSettings(
          bool notificationsEnabled,
          PrivacyLevel privacyLevel,
          DateTime utcNow,
          ThemeMode theme = ThemeMode.Light,
          string language = "en")
        {
            EnsureActive();

            var next = UserSettings.Create(theme, language, notificationsEnabled, privacyLevel);

            if (Equals(next, Settings)) return;

            Settings = next;
            Touch(utcNow);

            // domain event later
        }

        public void ChangeUsername(string newUsername, DateTime nowUtc)
        {
            EnsureActive();

            var next = Username.Create(newUsername);
            if (next == Username) return;

            var old = Username;
            Username = next;
            Touch(nowUtc);

            Raise(new UserUsernameChangedDomainEvent(Id, old, next, nowUtc));
        }

        public void ChangePasswordHash(string newPasswordHash, DateTime nowUtc)
        {
            EnsureActive();

            var next = PasswordHash.FromHash(newPasswordHash);
            if (next == PasswordHash) return;

            PasswordHash = next;
            Touch(nowUtc);

            Raise(new UserPasswordChangedDomainEvent(Id, nowUtc));
        }

        public void Deactivate(DateTime nowUtc)
        {
            if (Status == AccountStatus.Deleted) return;

            Status = AccountStatus.InActive;
            Touch(nowUtc);

            Raise(new UserDeactivatedDomainEvent(Id, nowUtc));
        }



        private void EnsureActive()
        {
            if (Status != AccountStatus.Active)
                throw new DomainException("User is not active.");
        }

        private void Touch(DateTime nowUtc) => LastModified = nowUtc;



    }
}
