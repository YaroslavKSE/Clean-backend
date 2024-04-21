using Othello.Application.Statistics;
using Web.Domain;

namespace Othello.Infrastructure.UserStatistics;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly InMemoryDatabase _database;

    public StatisticsRepository(InMemoryDatabase database)
    {
        _database = database;
    }

    public GameStatistics GetOrCreateStatistics(string username)
    {
        return _database.GetOrCreateStatistics(username);
    }

    public void UpdateStatistics(GameStatistics statistics)
    {
        _database.UpdateStatistics(statistics);
    }
}