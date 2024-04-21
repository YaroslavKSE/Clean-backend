using Othello.Application.Sessions;
using Web.Domain;

namespace Othello.Application.GameInterfaces;

public interface IGameRepository
{
    // Retrieves all games that are currently waiting for an opponent
    Task<IEnumerable<GameSession>> GetWaitingGameSessionsAsync();

    // Joins an existing game that is waiting for an opponent
    Task<bool> JoinGameSessionAsync(Guid gameId, PlayerInfo player);

    // Retrieves a game by its unique identifier
    Task<GameSession?> GetGameSessionByIdAsync(Guid gameId);

    Task SaveChatMessageAsync(ChatMessage message);
    Task<List<ChatMessage>> GetChatMessagesBySessionIdAsync(Guid gameId);
}