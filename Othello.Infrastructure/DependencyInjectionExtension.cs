using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Othello.Application.GameInterfaces;
using Othello.Application.Statistics;
using Othello.Application.UserInterfaces;
using Othello.Domain.Interfaces;
using Othello.Infrastructure.GameServices;
using Othello.Infrastructure.UserServices;
using Othello.Infrastructure.UserStatistics;

namespace Othello.Infrastructure;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register all infrastructure services here
        services.AddScoped<IUserStorage, UserStorage>();
        services.AddScoped<IUserExistChecker, UserExistChecker>();
        services.AddSingleton<IStatisticsRepository, StatisticsRepository>();
        services.AddScoped<IGameCreator, GameCreator>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddSingleton<IGameViewUpdater, ConsoleView>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenGenerator>(provider =>
        {
            var requiredService = provider.GetRequiredService<IConfiguration>();
            return new TokenGenerator(
                requiredService["Jwt:SecretKey"] ?? throw new InvalidOperationException(),
                requiredService["Jwt:Issuer"] ?? throw new InvalidOperationException(),
                requiredService["Jwt:Audience"] ?? throw new InvalidOperationException()
            );
        });

        // Add other infrastructure-specific services like database context, external services, etc.

        return services;
    }
}