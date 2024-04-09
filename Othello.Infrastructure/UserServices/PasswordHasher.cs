using Othello.Application.Interfaces;

namespace Othello.Infrastructure.UserServices;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        throw new NotImplementedException();
    }

    public bool VerifyPassword(string userPassword, string providedPassword)
    {
        throw new NotImplementedException();
    }
}