using Othello.Application.UserInterfaces;
using Othello.Domain;
using Web.Domain;

namespace Othello.Infrastructure.UserServices;

public class UserStorage : IUserStorage
{
    public async Task<bool> AddAsync(User user)
    {
        if (await InMemoryDatabase.ExistsUserAsync(user.Username))
            return false;  // User already exists
        await InMemoryDatabase.AddUserAsync(user);
        return true;
    }

    public Task<User?> FindByUsernameAsync(string username)
    {
        return InMemoryDatabase.GetUserByUsernameAsync(username);
    }
}