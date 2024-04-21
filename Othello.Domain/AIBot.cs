namespace Othello.Domain;

public class AIBot : Player
{
    private Game _game { get; set; }
    public AIBot(CellState color) : base(color)
    {

    }

    private readonly Random _random = new();

    // MakeMove is now an async method and returns a Task
    public override async Task MakeMoveAsync(Game gameBoard)
    {
        await SimulateAiDelayAsync();

        var availableMoves = gameBoard.Board.GetAvailableMoves(Color);
        if (availableMoves != null && availableMoves.Count > 0)
        {
            var randomInteger = _random.Next(0, availableMoves.Count);
            var randomMove = availableMoves[randomInteger];
            gameBoard.MakeMove(randomMove.Item1, randomMove.Item2);
        }
        else
        {
            // Fallback if no valid move found
            throw new InvalidOperationException("AI Bot could not find a valid move.");
        }
    }

    public override string GetTurnMessageNotification()
    {
        return $"AI Bot {Color} is thinking...";
    }

    private async Task SimulateAiDelayAsync()
    {
        // Introduce a random delay between 1 and 3 seconds
        var delay = _random.Next(1000, 3001); // Milliseconds
        await Task.Delay(delay);
    }
    private void OnOpponentMoved(object sender, MoveEventArgs e)
    {
        if (e.Player != this)
        {
            // Make a move asynchronously
            Task.Run(() => MakeMoveAsync(sender as Game ?? throw new InvalidOperationException()));
        }
    }

    public override void SetGameForBot(Game game)
    {
        _game = game;
        _game.MoveMade += OnOpponentMoved;
    }
}