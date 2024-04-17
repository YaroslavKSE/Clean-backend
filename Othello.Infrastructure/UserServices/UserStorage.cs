using Othello.Application.UserInterfaces;
using Web.Domain;

namespace Othello.Infrastructure.UserServices;

public class UserStorage : IUserStorage
{
    private readonly InMemoryDatabase _db;

    public UserStorage(InMemoryDatabase db)
    {
        _db = db;
    }
    
    public async Task<bool> AddAsync(User user)
    {
        if (await _db.ExistsUserAsync(user.Username))
            return false; // User already exists
        
        await _db.AddUserAsync(user);
        return true;
    }

    public Task<User?> FindByUsernameAsync(string username)
    {
        return _db.GetUserByUsernameAsync(username);
    }
}