namespace Othello.Application.Interfaces;

public interface IGameCreator
{
    Task<Guid> CreateGame(string userId, string opponentType, string opponentName);
}