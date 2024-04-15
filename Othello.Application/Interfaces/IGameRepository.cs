using Othello.Application.Sessions;
using Othello.Domain;

namespace Othello.Application.Interfaces;

public interface IGameRepository
{
    // Retrieves all games that are currently waiting for an opponent
    Task<IEnumerable<GameSession>> GetWaitingGameSessionsAsync();

    // Joins an existing game that is waiting for an opponent
    Task<bool> JoinGameSessionAsync(Guid gameId, string userId);

    // Retrieves a game by its unique identifier
    Task<Game?> GetGameByIdAsync(Guid gameId);
}