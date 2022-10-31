using Domain.Models;

namespace Discord;

public static class Extensions
{
    public static string Tag(this Player player)
    {
        return player.User?.Tag() ?? "";
    }
    
    public static string Tag(this User user)
    {
        if (user.DiscordId is null)
            return "";
        
        return $"<@{user.DiscordId}>";
    }
}