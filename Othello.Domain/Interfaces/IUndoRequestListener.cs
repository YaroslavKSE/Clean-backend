namespace Othello.Domain.Interfaces;

public interface IUndoRequestListener
{
    Task<bool> WaitForUndoAsync(Guid gameId);
}