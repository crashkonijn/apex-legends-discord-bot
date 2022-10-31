namespace Domain.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public ulong? DiscordId { get; set; }
    
    public Player[] Players { get; set; }
}