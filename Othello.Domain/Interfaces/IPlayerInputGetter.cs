namespace Othello.Domain.Interfaces;

public interface IPlayerInputGetter
{
    Task<(int row, int col)> WaitForMoveAsync(Guid gameId);
    void SetMove(Guid gameId, int row, int col);
}