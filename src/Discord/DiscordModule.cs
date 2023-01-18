using Discord.Commands;
using Discord.Commands.Support;
using Discord.WebSocket;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discord;

public static class DiscordModule
{
    public static void RegisterDiscordModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DiscordSocketClient>();
        services.AddSingleton<CommandService>();
        services.AddSingleton<IMessageService, MessageService>();

        services.AddSingleton<ISlashCommand, StatsCommand>();
        services.AddSingleton<ISlashCommand, RankingCommand>();
        services.AddSingleton<ISlashCommand, TrackedCommand>();
        services.AddSingleton<ISlashCommand, RefreshCommand>();
        
        services.Configure<DiscordOptions>(configuration.GetSection(DiscordOptions.ConfigurationEntry));
    }
}