using Othello.Domain.Interfaces;

namespace Othello.Domain;

public class HumanPlayer : Player
{
    public HumanPlayer(CellState color, IPlayerInputGetter inputGetter) : base(color)
    {
        _inputGetter = inputGetter;
    }

    private readonly IPlayerInputGetter _inputGetter;

    public override Task MakeMoveAsync(Game gameBoard)
    {
        return Task.CompletedTask;
    }


    public override string GetTurnMessageNotification()
    {
        return $"Player {Color}'s turn. Please enter your move (row col):";
    }

    public override void SetGameForBot(Game game)
    {
        throw new InvalidOperationException("Human is not bot");
    }
}