using Othello.Application.Statistics;
using Othello.Domain;
using Othello.Domain.Interfaces;

namespace Othello.Application.Sessions;

public class GameSession
{
    public Guid GameId { get; set; }
    public Game Game { get; private set; }
    public List<PlayerInfo> Players { get; private set; } = [];
    public DateTime StartTime { get; private set; }

    private readonly IStatisticsRepository _statisticsRepository;

    public GameSession(PlayerInfo player1Info, PlayerInfo? player2Info, IGameViewUpdater observer, IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
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
        var moveMade = Game.MakeMove(row, col);
        var gameOver = CheckGameOver();
        if (gameOver)
        {
            Game.EndGame();
            UpdateGameStatistics();
        }

        return moveMade;

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

    public bool CheckGameOver()
    {
        return Game.CheckGameOver();
    }

    public bool UndoMove()
    {
        return Game.UndoMove();
    }

    private Dictionary<CellState, int> GetScore()
    {
        return Game.CalculateScore();
    }
    private void UpdateGameStatistics()
    {
        // Assuming you have some sort of repository or service where you manage statistics.
        var player1Stats = _statisticsRepository.GetOrCreateStatistics(Players[0].WebUsername);
        var player2Stats = _statisticsRepository.GetOrCreateStatistics(Players[1].WebUsername);

        var finalScore = GetScore();
        if (finalScore[CellState.Black] > finalScore[CellState.White])
        {
            player1Stats.RecordWin();
            player2Stats.RecordLoss();
        }
        else if (finalScore[CellState.Black] < finalScore[CellState.White])
        {
            player1Stats.RecordLoss();
            player2Stats.RecordWin();
        }
        else
        {
            player1Stats.RecordDraw();
            player2Stats.RecordDraw();
        }

        _statisticsRepository.UpdateStatistics(player1Stats);
        _statisticsRepository.UpdateStatistics(player2Stats);
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