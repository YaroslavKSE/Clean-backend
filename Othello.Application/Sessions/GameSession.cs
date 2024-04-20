using Othello.Domain;
using Othello.Domain.Interfaces;

namespace Othello.Application.Sessions;

public class GameSession
{
    public Guid GameId { get; set; }
    public Game Game { get; private set; }
    public List<PlayerInfo> Players { get; private set; } = [];
    public DateTime StartTime { get; private set; }
    public bool IsGameOver => Game.IsGameOver;

    public GameSession(PlayerInfo player1Info, PlayerInfo? player2Info, IGameViewUpdater observer)
    {
        GameId = Guid.NewGuid();
        if (player2Info?.OthelloPlayer != null)
        {
            Game = new Game(player1Info.OthelloPlayer, player2Info.OthelloPlayer, observer);
        }
        else
        {
            Game = new Game(player1Info.OthelloPlayer, null, observer);   
        }
        
        StartTime = DateTime.UtcNow;
        Players.Add(player1Info); 
        if (player2Info != null) {
            Players.Add(player2Info); 
        }
    }
    
    public bool MakeMove(int row, int col)
    {
        return Game.MakeMove(row, col);
    }

    public void AddSecondWebPlayer(Player secondPlayer)
    {
        Game.AddSecondPlayer(secondPlayer);
    }

    public void StartGame()
    {
        Game.Start();
    }

    public IEnumerable<(int, int)> ShowHints()
    {
        return Game.ShowHints();
    }

    public bool UndoMove()
    {
        return Game.UndoMove();
    }

    public Dictionary<CellState, int> GetScore()
    {
        return Game.CalculateScore();
    }
}

public class PlayerInfo
{
    public string WebUsername { get; set; }
    public Player OthelloPlayer { get; set; }

    public PlayerInfo(string username, Player player)
    {
        WebUsername = username;
        OthelloPlayer = player;
    }
}