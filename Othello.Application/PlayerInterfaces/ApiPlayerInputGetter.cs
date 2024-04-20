using System.Collections.Concurrent;
using Othello.Domain.Interfaces;

namespace Othello.Application.PlayerInterfaces;

public class ApiPlayerInputGetter : IPlayerInputGetter
{
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<(int, int)>> _pendingMoves = new();
    
    public void SetMove(Guid gameId, int row, int col)
    {
        var source = _pendingMoves.GetOrAdd(gameId, _ => new TaskCompletionSource<(int, int)>());
        source.SetResult((row, col));
    }

    public Task<(int row, int col)> WaitForMoveAsync(Guid gameId)
    {
        if (_pendingMoves.TryGetValue(gameId, out var source)) return source.Task;

        var newSource = new TaskCompletionSource<(int, int)>();
        _pendingMoves[gameId] = newSource;
        return newSource.Task;
    }
}