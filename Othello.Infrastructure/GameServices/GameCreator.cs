using Othello.Application.GameInterfaces;
using Othello.Application.Sessions;

namespace Othello.Infrastructure.GameServices;

public class GameCreator : IGameCreator
{
    public Task<GameSession> CreateGameSession(string userId, string opponentType, string opponentName)
    {
        throw new NotImplementedException();
    }

    public Task UpdateGameSessionAsync(GameSession session)
    {
        throw new NotImplementedException();
    }
}