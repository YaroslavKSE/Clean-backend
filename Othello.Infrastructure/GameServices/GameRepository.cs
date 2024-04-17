using Othello.Application.GameInterfaces;
using Othello.Application.Sessions;
using Othello.Domain;

namespace Othello.Infrastructure.GameServices;

public class GameRepository : IGameRepository
{
    public Task<IEnumerable<GameSession>> GetWaitingGameSessionsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> JoinGameSessionAsync(Guid gameId, PlayerInfo player)
    {
        throw new NotImplementedException();
    }

    public Task<GameSession?> GetGameSessionByIdAsync(Guid gameId)
    {
        throw new NotImplementedException();
    }
}