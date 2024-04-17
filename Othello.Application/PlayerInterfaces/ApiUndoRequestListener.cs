using System.Collections.Concurrent;
using Othello.Domain.Interfaces;

namespace Othello.Application.PlayerInterfaces;

public class ApiUndoRequestListener : IUndoRequestListener
{
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<bool>> _pendingUndos = new();

    public void RequestUndo(Guid gameId)
    {
        if (_pendingUndos.TryGetValue(gameId, out var source))
        {
            source.SetResult(true);
            _pendingUndos.TryRemove(gameId, out var _);
        }
    }

    public Task<bool> WaitForUndoAsync(Guid gameId)
    {
        if (_pendingUndos.TryGetValue(gameId, out var source))
        {
            return source.Task;
        }

        var newSource = new TaskCompletionSource<bool>();
        _pendingUndos[gameId] = newSource;
        return newSource.Task;
    }
}