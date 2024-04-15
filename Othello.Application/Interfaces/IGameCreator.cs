namespace Othello.Application.Interfaces;

public interface IGameCreator
{
    Task<Guid> CreateGameSession(string userId, string opponentType, string opponentName);
}