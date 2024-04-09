using Microsoft.Extensions.DependencyInjection;
using Othello.Application.Interfaces;
using Othello.Infrastructure.UserServices;

namespace Othello.Infrastructure;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Register all infrastructure services here
        services.AddScoped<IUserStorage, UserStorage>();
        services.AddScoped<IUserExistChecker, UserExistChecker>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        // Add other infrastructure-specific services like database context, external services, etc.

        return services;
    }
}
