using Web.Domain;

namespace Othello.Application.UserInterfaces;

public interface IUserStorage
{
    Task<bool> AddAsync(User user);
    Task<User?> FindByUsernameAsync(string userId);
}