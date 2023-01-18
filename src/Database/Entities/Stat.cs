namespace Database.Entities;

public class Stat
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    
    public int Season { get; set; }
    public int Split { get; set; }

    public string Legend { get; set; }
    public int Rank { get; set; }
    public string RankName { get; set; }
    public int Kills { get; set; }

    public Player Player { get; set; }
}