using System.Collections.Concurrent;
using Othello.Domain;
using Web.Domain;

namespace Othello.Infrastructure;

public class InMemoryDatabase
{
    // Using ConcurrentDictionary for thread-safe operations
    private static readonly ConcurrentDictionary<Guid, Game> Games = new();
    private static readonly ConcurrentDictionary<string, User> Users = new();
    public static Task<Guid> AddGameAsync(Game game)
    {
        var gameId = Guid.NewGuid();
        game.Id = gameId;
        Games[gameId] = game;
        return Task.FromResult(gameId);
    }

    // public static Task<IEnumerable<Game>> GetWaitingGamesAsync()
    // {
    //     var waitingGames = Games.Values.Where(g => !g.HasStarted).ToList();
    //     return Task.FromResult<IEnumerable<Game>>(waitingGames);
    // }

    public static Task<Game?> GetGameByIdAsync(Guid gameId)
    {
        Games.TryGetValue(gameId, out var game);
        return Task.FromResult(game);
    }

    public static Task<bool> UpdateGameAsync(Game game)
    {
        Games[game.Id] = game;
        return Task.FromResult(true);
    }

    // User Operations
    public static Task AddUserAsync(User user)
    {
        Users[user.Username] = user; // Assuming User has a Username property
        return Task.CompletedTask;
    }

    public static Task<User?> GetUserByUsernameAsync(string username)
    {
        Users.TryGetValue(username, out var user);
        return Task.FromResult(user);
    }

    public static Task<bool> ExistsUserAsync(string username)
    {
        return Task.FromResult(Users.ContainsKey(username));
    }
}