using Othello.Application.UserInterfaces;
using Othello.Domain;
using Web.Domain;

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