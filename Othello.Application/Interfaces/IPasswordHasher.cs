namespace Othello.Application.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string userPassword, string providedPassword);
}