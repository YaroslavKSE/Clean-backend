using Othello.Application.GameInterfaces;
using Othello.Application.Sessions;
using Web.Domain;

namespace Othello.Infrastructure.GameServices;

public class GameRepository : IGameRepository
{
    private readonly InMemoryDatabase _db;

    public GameRepository(InMemoryDatabase db)
    {
        _db = db;
    }

    public async Task<IEnumerable<GameSession>> GetWaitingGameSessionsAsync()
    {
        var waitingSessions = await _db.GetWaitingGameSessionsAsync();
        return waitingSessions;
    }

    public async Task<bool> JoinGameSessionAsync(Guid gameId, PlayerInfo player)
    {
        var joined = await _db.JoinGameSessionAsync(gameId, player);
        return joined;
    }

    public async Task<GameSession?> GetGameSessionByIdAsync(Guid gameId)
    {
        var session = await _db.GetGameSessionByIdAsync(gameId);
        return session;
    }

    public Task SaveChatMessageAsync(ChatMessage message)
    {
        _db.SaveChatMessageAsync(message);
        return Task.CompletedTask;
    }

    public async Task<List<ChatMessage>> GetChatMessagesBySessionIdAsync(Guid gameSessionId)
    {
        var chat = await _db.GetChatMessagesBySessionIdAsync(gameSessionId);
        return chat;
    }
}