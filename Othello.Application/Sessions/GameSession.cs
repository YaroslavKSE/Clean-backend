using Othello.Domain;
using Othello.Domain.Interfaces;
using Web.Domain;

namespace Othello.Application.Sessions
{
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
            Game = new Game(player1Info.OthelloPlayer, player1Info.OthelloPlayer, observer);
            StartTime = DateTime.UtcNow;
            Players.Add(player1Info); // Add first player info
            if (player2Info != null)
            {
                Players.Add(player2Info); // Add second player info if exists
            }
        }
        // Wrapper methods to interact with the Game object
        public bool MakeMove(int row, int col) => Game.MakeMove(row, col);

        public void StartGame() => Game.Start();

        public IEnumerable<(int, int)> ShowHints() => Game.ShowHints();

        public bool UndoMove() => Game.UndoMove();

        public Dictionary<CellState, int> GetScore() => Game.CalculateScore();
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
}