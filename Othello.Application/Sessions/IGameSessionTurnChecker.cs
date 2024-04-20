namespace Othello.Application.Sessions;

public interface IGameSessionTurnChecker
{
    Task CheckAndExecuteTurn(Guid gameId);
}