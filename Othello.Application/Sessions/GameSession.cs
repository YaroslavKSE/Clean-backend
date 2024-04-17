using Othello.Domain;
using Othello.Domain.Interfaces;

namespace Othello.Application.Sessions
{
    public class GameSession
    {
        public Guid GameId { get; set; }
        public Game Game { get; private set; }
        // Store player information in a list
        public List<PlayerInfo> Players { get; private set; } = [];
        public DateTime StartTime { get; private set; }
        public bool IsGameOver => Game.IsGameOver;

        public GameSession(Player player1, Player player2, IGameViewUpdater observer, PlayerInfo player1Info, PlayerInfo? player2Info = null)
        {
            GameId = Guid.NewGuid();
            Game = new Game(player1, player2, observer);
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
        public Guid PlayerId { get; set; }
        public string Username { get; set; }
        public bool IsAI { get; set; }

        public PlayerInfo(Guid playerId, string username, bool isAI)
        {
            PlayerId = playerId;
            Username = username;
            IsAI = isAI;
        }
    }
    
}