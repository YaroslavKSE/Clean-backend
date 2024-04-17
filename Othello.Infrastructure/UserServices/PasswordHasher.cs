using Othello.Application.UserInterfaces;

namespace Othello.Infrastructure.UserServices;

public class PasswordHasher : IPasswordHasher
{
    
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string userPassword, string providedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(providedPassword, userPassword);
    }
}