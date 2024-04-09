using Othello.Application.Interfaces;

namespace Othello.Infrastructure.UserServices;

public class UserExistChecker : IUserExistChecker
{
    public Task<bool> ExistsAsync(string id)
    {
        throw new NotImplementedException();
    }
}