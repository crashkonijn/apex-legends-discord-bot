namespace Database.Entities;

public class Season
{
    public string Id
    {
        get => $"{this.Number}-{this.Split}";
        set {}
    }
    public int Number { get; set; } = 0;
    public int Split { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}