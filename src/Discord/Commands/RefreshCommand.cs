using Discord.Commands.Support;
using Discord.WebSocket;
using Domain.Interfaces;

namespace Discord.Commands;

public class RefreshCommand : SlashCommandBase
{
    private readonly IStatsService _statsService;
    public override string Name => "refresh";

    public RefreshCommand(IStatsService statsService)
    {
        this._statsService = statsService;
    }
    
    public override SlashCommandBuilder Init()
    {
        return this.GetBuilder()
            .WithDescription("Get your apex legends stats")
            .AddOption("username", ApplicationCommandOptionType.String, "The origin name", true);
    }

    public override async Task Handle(SocketSlashCommand command)
    {
        var username = command.Data.Options.First().Value.ToString();

        if (string.IsNullOrEmpty(username))
        {
            await command.RespondAsync("Please provide a username");
            return;
        }
        
        await this._statsService.LoadAndCompare(username);
        
        var stats = await this._statsService.GetStats(username);
        
        await command.RespondAsync($"Username: {stats.Username}\nLegend: {stats.Legend}\nRank: {stats.RankName} ({stats.Rank})\nKills: {stats.Kills}");
    }
}