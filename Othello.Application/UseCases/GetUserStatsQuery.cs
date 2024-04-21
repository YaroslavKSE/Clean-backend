using MediatR;
using Othello.Application.Statistics;

namespace Othello.Application.UseCases;
public class GetUserStatsQuery : IRequest<UserStatsResult>
{
    public string Username { get; set; }
}

public class UserStatsResult
{
    public int TotalGamesPlayed { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Draws { get; set; }
}

public class GetUserStatsQueryHandler : IRequestHandler<GetUserStatsQuery, UserStatsResult>
{
    private readonly IStatisticsRepository _statisticsRepository;

    public GetUserStatsQueryHandler(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public Task<UserStatsResult> Handle(GetUserStatsQuery request, CancellationToken cancellationToken)
    {
        var stats = _statisticsRepository.GetOrCreateStatistics(request.Username);

        var result = new UserStatsResult
        {
            TotalGamesPlayed = stats.TotalGamesPlayed,
            Wins = stats.Wins,
            Losses = stats.Losses,
            Draws = stats.Draws
        };

        return Task.FromResult(result);
    }
}

