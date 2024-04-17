using Othello.Application.Sessions;

namespace Othello.Application.GameInterfaces;

public interface IGameCreator
{
    Task<GameSession> CreateGameSession(string username, string opponentType);
}