using Web.Domain;

namespace Othello.Application.Statistics;

public interface IStatisticsRepository
{
    GameStatistics GetOrCreateStatistics(string username);
    void UpdateStatistics(GameStatistics statistics);
}