using Othello.Application.Interfaces;
using Othello.Domain;

namespace Othello.Infrastructure.UserServices;

public class UserStorage : IUserStorage
{
    public Task<bool> AddAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindByUsernameAsync(string userId)
    {
        throw new NotImplementedException();
    }
}