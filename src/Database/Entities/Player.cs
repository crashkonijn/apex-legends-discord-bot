using Domain.Enums;

namespace Database.Entities;

public class Player
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? UserId { get; set; }
    
    public string Username { get; set; }
    public Platform Platform { get; set; } = Platform.Origin;
    public bool IsTracking { get; set; } = false;
    
    public User? User { get; set; }
    public List<Stat> Stats { get; set; }
}