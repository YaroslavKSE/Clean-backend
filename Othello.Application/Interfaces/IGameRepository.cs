using Othello.Domain;

namespace Othello.Application.Interfaces;

public interface IGameRepository
{
    // Retrieves all games that are currently waiting for an opponent
    Task<IEnumerable<Game>> GetWaitingGamesAsync();

    // Joins an existing game that is waiting for an opponent
    Task<bool> JoinGameAsync(Guid gameId, string userId);

    // Retrieves a game by its unique identifier
    Task<Game?> GetGameByIdAsync(Guid gameId);
}