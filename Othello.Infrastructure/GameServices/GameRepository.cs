using Othello.Application.Interfaces;
using Othello.Domain;

namespace Othello.Infrastructure.GameServices;

public class GameRepository : IGameRepository
{
    public Task<IEnumerable<Game>> GetWaitingGamesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> JoinGameAsync(Guid gameId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Game?> GetGameByIdAsync(Guid gameId)
    {
        throw new NotImplementedException();
    }
}