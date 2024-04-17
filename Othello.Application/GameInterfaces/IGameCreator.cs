using Othello.Application.Sessions;

namespace Othello.Application.GameInterfaces;

public interface IGameCreator
{
    Task<GameSession> CreateGameSession(string userId, string opponentType, string opponentName);
}