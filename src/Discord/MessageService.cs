using Discord.WebSocket;
using Domain;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Options;

namespace Discord;

public class MessageService : IMessageService
{
    private readonly DiscordSocketClient _client;
    private readonly IStatCompareService _statCompareService;
    private readonly DiscordOptions _options;

    public MessageService(DiscordSocketClient client, IOptions<DiscordOptions> options, IStatCompareService statCompareService)
    {
        this._client = client;
        this._statCompareService = statCompareService;
        this._options = options.Value;
    }

    private SocketTextChannel? GetChannel()
    {
        if (this._client.ConnectionState != ConnectionState.Connected)
            return null;
        
        var guild = this._client.GetGuild(this._options.GuildId);
        return guild.TextChannels.Single(ch => ch.Name == this._options.ChannelName);
    }
    
    public async Task RankUpdate(Player player, Stat newStat, Stat? oldStat)
    {
        Console.WriteLine($"RankUpdate: {player.Username}");

        var channel = this.GetChannel();
        
        if (channel == null)
            return;

        if (this._statCompareService.IsNewSeasonOrSplit(oldStat, newStat))
        {
            await channel.SendMessageAsync($"{player.Tag()} with {player.Username} started this split on {newStat.RankName} with a total score of {newStat.Rank}! 🥰");
            return;
        }

        if (this._statCompareService.IsRankDown(oldStat, newStat))
        {
            await channel.SendMessageAsync($"{player.Tag()} with {player.Username} went down to {newStat.RankName}. What a loser!");
            return;
        }
        
        if (this._statCompareService.IsRankUp(oldStat, newStat))
        {
            await channel.SendMessageAsync($"{player.Tag()} with {player.Username} has reached {newStat.RankName} with a total score of {newStat.Rank} 🎉");
        }
    }

    public async Task NewSeason(Season currentSeason, Season nextSeason)
    {
        var channel = this.GetChannel();
        
        if (channel == null)
            return;
        
        await channel.SendMessageAsync($"New season detected. Good luck in season {nextSeason.Number}!");
    }

    public async Task NewSplit(Season currentSeason, Season nextSeason)
    {
        var channel = this.GetChannel();
        
        if (channel == null)
            return;
        
        await channel.SendMessageAsync($"New season split detected. Good luck boys!");
    }
}