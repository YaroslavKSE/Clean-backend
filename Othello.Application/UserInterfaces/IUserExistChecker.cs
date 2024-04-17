namespace Othello.Application.UserInterfaces;

public interface IUserExistChecker
{
    Task<bool> ExistsAsync(string id);
}