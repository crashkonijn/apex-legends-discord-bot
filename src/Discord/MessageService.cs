using Discord.WebSocket;
using Domain;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Options;

namespace Discord;

public class MessageService : IMessageService
{
    private readonly DiscordSocketClient _client;
    private readonly DiscordOptions _options;

    public MessageService(DiscordSocketClient client, IOptions<DiscordOptions> options)
    {
        this._client = client;
        this._options = options.Value;
    }
    
    public async Task RankUpdate(Player player)
    {
        Console.WriteLine($"RankUpdate: {player.Username}");

        if (this._client.ConnectionState != ConnectionState.Connected)
            return;
        
        var guild = this._client.GetGuild(this._options.GuildId);
        var channel = guild.TextChannels.Single(ch => ch.Name == this._options.ChannelName);

        var stat = player.Stats.CurrentSeason();

        await channel.SendMessageAsync($"{player.Tag()} with {player.Username} has reached {stat.RankName} with a total score of {stat.Rank} 🎉");
    }
}