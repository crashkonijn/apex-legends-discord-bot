using Discord.Commands.Support;
using Discord.WebSocket;
using Domain;
using Domain.Interfaces;
using Domain.Models;

namespace Discord.Commands;

public class RankingCommand : SlashCommandBase
{
    private readonly IStatsService _statsService;
    private readonly IPlayerRepository _playerRepository;
    public override string Name => "ranking";

    public RankingCommand(IStatsService statsService, IPlayerRepository playerRepository)
    {
        this._statsService = statsService;
        this._playerRepository = playerRepository;
    }
    
    public override SlashCommandBuilder Init()
    {
        return this.GetBuilder()
            .WithDescription("Get current season ranking");
    }

    public override async Task Handle(SocketSlashCommand command)
    {
        var players = await this._playerRepository.GetTracked();

        if (!players.Any())
        {
            await command.RespondAsync("No players found");
            return;
        }

        if (!players.Any(x => x.Stats.CurrentSeason() is not null))
        {
            await command.RespondAsync("No players with stats found");
            return;
        }

        var ordered = players.OrderByDescending(x => x.Stats.CurrentSeason()?.Rank);

        var text = string.Join("\n", ordered.Select(this.ToRank));
        
        await command.RespondAsync(text);
    }

    private string ToRank(Player player, int index)
    {
        index++;

        var stat = player.Stats.CurrentSeason();

        return $"{this.GetIcon(index)}: {player.Username} {stat?.RankName} ({stat?.Rank})";
    }

    private string GetIcon(int index)
    {
        return index switch
        {
            1 => "🥇",
            2 => "🥈",
            3 => "🥉",
            _ => $"{index}"
        };
    }
}