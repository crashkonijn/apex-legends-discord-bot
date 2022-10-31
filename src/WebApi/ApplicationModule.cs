using Application;
using Application.Services;
using Database;
using Discord;
using Domain;
using Tracker;

namespace WebApi;

public static class ApplicationModule
{
    public static void RegisterModules(this IServiceCollection services, IConfiguration config)
    {
        services.RegisterDomainModule();
        services.RegisterApplicationModule(config);
        services.RegisterTrackerModule(config);
        services.RegisterDatabaseModule(config);
        services.RegisterDiscordModule(config);
    }

    public static void RegisterBackgroundServices(this IServiceCollection services)
    {
        services.AddSingleton<StatsBackgroundService>();
        services.AddSingleton<BotBackgroundService>();
        
        services.AddHostedService<MasterBackgroundService>();
    }
}