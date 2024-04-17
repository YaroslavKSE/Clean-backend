using Othello.Application.UserInterfaces;

namespace Othello.Infrastructure.UserServices;

public class UserExistChecker : IUserExistChecker
{
    public Task<bool> ExistsAsync(string username)
    {
        return InMemoryDatabase.ExistsUserAsync(username);
    }
}