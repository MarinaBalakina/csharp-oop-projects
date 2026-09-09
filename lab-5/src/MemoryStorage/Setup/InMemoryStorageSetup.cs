using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;
using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.Security;
using Itmo.ObjectOrientedProgramming.Lab5.MemoryStorage.Data;
using Itmo.ObjectOrientedProgramming.Lab5.MemoryStorage.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.MemoryStorage.Setup;

public static class InMemoryStorageSetup
{
    public static IServiceCollection AddInMemoryStorage(this IServiceCollection services)
    {
        services.AddSingleton<IAccountStorage, InMemoryAccount>();
        services.AddSingleton<ISessionStorage, InMemorySession>();
        services.AddSingleton<IHistoryStorage, InMemoryHistory>();
        services.AddSingleton<ISystemPassword>(static _ => new SystemPasswordProvider("your password"));

        return services;
    }
}