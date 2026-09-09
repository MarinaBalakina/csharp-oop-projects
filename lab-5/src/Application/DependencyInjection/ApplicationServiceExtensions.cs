using Itmo.ObjectOrientedProgramming.Lab5.Application.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Application.History;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Sessions;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.DependencyInjection;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<AccountService>();
        services.AddScoped<SessionService>();
        services.AddScoped<HistoryService>();

        return services;
    }
}