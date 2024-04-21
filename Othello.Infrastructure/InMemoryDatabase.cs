using System.Collections.Concurrent;
using Othello.Application.Sessions;
using Othello.Domain;
using Web.Domain;

namespace Othello.Infrastructure;

public class InMemoryDatabase
{
    // Using ConcurrentDictionary for thread-safe operations
    private static readonly ConcurrentDictionary<Guid, GameSession> Games = new();
    private static readonly ConcurrentDictionary<string, User> Users = new();
    private static readonly ConcurrentDictionary<string, GameStatistics> Statistics = new();
    private static readonly ConcurrentDictionary<Guid, List<ChatMessage>> ChatMessages = new();
    
    // User Operations
    public Task AddUserAsync(User user)
    {
        Users[user.Username] = user; 
        return Task.CompletedTask;
    }

    public Task<User?> GetUserByUsernameAsync(string username)
    {
        Users.TryGetValue(username, out var user);
        return Task.FromResult(user);
    }

    public Task<bool> ExistsUserAsync(string username)
    {
        return Task.FromResult(Users.ContainsKey(username));
    }

    // GameSession Operations
    public void AddGameSessionAsync(GameSession session)
    {
        Games[session.GameId] = session; // Overwrites existing session
    }

    public Task<IEnumerable<GameSession>> GetWaitingGameSessionsAsync()
    {
        // Filter game sessions where a second player has not yet joined
        var waitingSessions = Games.Values.Where(session => session.Players.Count < 2);
        return Task.FromResult(waitingSessions);
    }

    public Task<bool> JoinGameSessionAsync(Guid gameId, PlayerInfo player)
    {
        if (Games.TryGetValue(gameId, out var session))
            // Ensure the game is not full and is waiting for a player
            if (session.Players.Count < 2)
            {
                session.Players.Add(player);
                return Task.FromResult(true);
            }

        return Task.FromResult(false);
    }

    public Task<GameSession?> GetGameSessionByIdAsync(Guid gameId)
    {
        Games.TryGetValue(gameId, out var session);
        return Task.FromResult(session);
    }
    
    public GameStatistics GetOrCreateStatistics(string username)
    {
        return Statistics.GetOrAdd(username, new GameStatistics(Guid.NewGuid()));
    }

    public void UpdateStatistics(GameStatistics updatedStatistics)
    {
        Statistics[updatedStatistics.UserId.ToString()] = updatedStatistics;
    }
    
    public void SaveChatMessageAsync(ChatMessage message)
    {
        if (!ChatMessages.ContainsKey(message.GameSessionId))
        {
            // If no list exists for this game, create one and add the message
            ChatMessages[message.GameSessionId] = new List<ChatMessage> { message };
        }
        else
        {
            // If a list already exists, add the message to it
            ChatMessages[message.GameSessionId].Add(message);
        }
    }
    public Task<List<ChatMessage>> GetChatMessagesBySessionIdAsync(Guid gameSessionId)
    {
        if (ChatMessages.TryGetValue(gameSessionId, out var messages))
        {
            return Task.FromResult(messages.ToList());
        }
        return Task.FromResult(new List<ChatMessage>());
    }

}