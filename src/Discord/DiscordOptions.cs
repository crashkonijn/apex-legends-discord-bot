namespace Discord;

public class DiscordOptions
{
    public const string ConfigurationEntry = "DiscordModule";

    public string Token { get; set; }
    public ulong GuildId { get; set; }
    public string ChannelName { get; set; }
}