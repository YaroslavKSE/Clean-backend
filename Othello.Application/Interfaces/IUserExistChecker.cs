namespace Othello.Application.Interfaces;

public interface IUserExistChecker
{
    Task<bool> ExistsAsync(string id);
}