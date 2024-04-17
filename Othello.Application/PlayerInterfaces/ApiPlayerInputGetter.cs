using System.Collections.Concurrent;
using Othello.Domain.Interfaces;

namespace Othello.Application.PlayerInterfaces;

public class ApiPlayerInputGetter : IPlayerInputGetter
{
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<(int, int)>> _pendingMoves = new();

    public ApiPlayerInputGetter()
    {
    }

    public void AddMoveRequest(Guid gameId)
    {
        _pendingMoves.TryAdd(gameId, new TaskCompletionSource<(int, int)>());
    }

    public void SetMove(Guid gameId, int row, int col)
    {
        if (_pendingMoves.TryGetValue(gameId, out var source))
        {
            source.SetResult((row, col));
            _pendingMoves.TryRemove(gameId, out _);
        }
    }

    public Task<(int row, int col)> WaitForMoveAsync(Guid gameId)
    {
        if (_pendingMoves.TryGetValue(gameId, out var source)) return source.Task;

        var newSource = new TaskCompletionSource<(int, int)>();
        _pendingMoves[gameId] = newSource;
        return newSource.Task;
    }
}