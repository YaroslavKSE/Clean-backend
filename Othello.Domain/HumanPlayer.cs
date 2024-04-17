using Othello.Domain.Interfaces;

namespace Othello.Domain;

public class HumanPlayer : Player
{
    public HumanPlayer(CellState color, IPlayerInputGetter inputGetter) : base(color)
    {
        _inputGetter = inputGetter;
    }

    private readonly IPlayerInputGetter _inputGetter;

    public override async Task MakeMoveAsync(Guid gameId, Game gameBoard)
    {
        var (row, col) = await _inputGetter.WaitForMoveAsync(gameId);
        gameBoard.MakeMove(row - 1, col - 1);
    }


    public override string GetTurnMessageNotification()
    {
        return $"Player {Color}'s turn. Please enter your move (row col):";
    }
}