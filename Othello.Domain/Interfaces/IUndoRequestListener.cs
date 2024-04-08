namespace Othello.Domain.Interfaces;

public interface IUndoRequestListener
{
    Task<bool> UndoKeyPressedAsync(CancellationToken ctsToken);
}