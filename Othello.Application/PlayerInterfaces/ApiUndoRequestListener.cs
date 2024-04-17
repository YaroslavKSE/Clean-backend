using System.Collections.Concurrent;
using Othello.Domain.Interfaces;

namespace Othello.Application.PlayerInterfaces;

public class ApiUndoRequestListener : IUndoRequestListener
{
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<bool>> _pendingUndos = new();
    public ApiUndoRequestListener()
    {
        
    }
    public void RequestUndo(Guid gameId)
    {
        var source = _pendingUndos.GetOrAdd(gameId, _ => new TaskCompletionSource<bool>());
        source.TrySetResult(true); // This ensures that multiple requests don't overwrite each other
    }

    public Task<bool> WaitForUndoAsync(Guid gameId)
    {
        var source = _pendingUndos.GetOrAdd(gameId, _ => new TaskCompletionSource<bool>());
        return source.Task;
    }
}
