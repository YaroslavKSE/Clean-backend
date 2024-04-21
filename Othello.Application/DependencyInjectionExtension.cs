using Microsoft.Extensions.DependencyInjection;
using Othello.Application.PlayerInterfaces;
using Othello.Application.Sessions;
using Othello.Domain.Interfaces;

namespace Othello.Application;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register all infrastructure services here
        services.AddSingleton<IPlayerInputGetter, ApiPlayerInputGetter>();
        services.AddSingleton<IUndoRequestListener, ApiUndoRequestListener>();
        return services;
    }
}