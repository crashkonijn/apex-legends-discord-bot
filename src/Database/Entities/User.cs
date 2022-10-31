namespace Database.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public ulong? DiscordId { get; set; }
    
    public List<Player> Players { get; set; }
}