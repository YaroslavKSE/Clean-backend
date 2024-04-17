using MediatR;
using Othello.Application.GameInterfaces;

namespace Othello.Application.UseCases;

public class GetHintsQuery : IRequest<IEnumerable<(int Row, int Column)>>
{
    public Guid GameId { get; set; }
}

public class GetHintsQueryHandler : IRequestHandler<GetHintsQuery, IEnumerable<(int Row, int Column)>>
{
    private readonly IGameRepository _gameRepository;

    public GetHintsQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<IEnumerable<(int Row, int Column)>> Handle(GetHintsQuery request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetGameSessionByIdAsync(request.GameId);
        if (game == null)
        {
            return new List<(int, int)>(); // Or optionally throw an exception
        }

        // Assuming Game has a method to get hints for the current player
        var hints = game.ShowHints();
        return hints;
    }
}