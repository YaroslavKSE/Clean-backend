using Othello.Application.UserInterfaces;

namespace Othello.Infrastructure.UserServices;

public class UserExistChecker : IUserExistChecker
{
    private readonly InMemoryDatabase _db;

    public UserExistChecker(InMemoryDatabase db)
    {
        _db = db;
    }

    public Task<bool> ExistsAsync(string username)
    {
        return _db.ExistsUserAsync(username);
    }
}