namespace Identity.Application.Abstractions.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string Password, string Hashpassword);
    }
}
