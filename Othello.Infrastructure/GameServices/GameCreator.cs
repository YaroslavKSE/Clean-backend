using Othello.Application.GameInterfaces;
using Othello.Application.Sessions;
using Othello.Domain;
using Othello.Domain.Interfaces;

namespace Othello.Infrastructure.GameServices;

public class GameCreator : IGameCreator
{
    private readonly InMemoryDatabase _db;
    private readonly IPlayerInputGetter _playerInputGetter;
    private readonly IUndoRequestListener _undoRequestListener;
    private readonly IGameViewUpdater _gameViewUpdater;

    public GameCreator(InMemoryDatabase db, IPlayerInputGetter playerInputGetter,
        IUndoRequestListener undoRequestListener, IGameViewUpdater gameViewUpdater)
    {
        _db = db;
        _playerInputGetter = playerInputGetter;
        _undoRequestListener = undoRequestListener;
        _gameViewUpdater = gameViewUpdater;
    }

    public Task<GameSession> CreateGameSession(string username, string opponentType)
    {
        var player1 = new HumanPlayer(CellState.Black, _playerInputGetter);
        Player player2;

        if (opponentType.ToLower() == "bot")
        {
            player2 = new AIBot(CellState.White); // Assume AI player
        }
        else
            // Create a placeholder for the second human player, which will be replaced when a real player joins
            player2 = new HumanPlayer(CellState.White, _playerInputGetter);

        var player1Info = new PlayerInfo(username, player1);
        var player2Info = opponentType.ToLower() == "bot" ? new PlayerInfo("AiBot", player2) : null;

        var session = new GameSession(player1Info, player2Info, _gameViewUpdater);
        if (opponentType.ToLower() == "bot")
        {
            player2.SetGameForBot(session.Game);
        }
        _db.AddGameSessionAsync(session);
        return Task.FromResult(session);
    }
}