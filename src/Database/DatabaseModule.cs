using Database.Repositories;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database;

public static class DatabaseModule
{
    public static void RegisterDatabaseModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(typeof(DatabaseModule));
        services.AddSingleton<IStatsRepository, StatsRepository>();
        services.AddSingleton<IPlayerRepository, PlayerRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        
        services.AddDbContextFactory<StatsContext>();
    }
}