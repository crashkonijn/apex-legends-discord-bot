namespace Domain.Models;

public class Season
{
    public string Id => $"{this.Number}-{this.Split}";
    public int Number { get; set; } = 0;
    public int Split { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}