using Domain.Models;

namespace Domain;

public static class Extensions
{
    public static Stat? CurrentSeason(this IEnumerable<Stat> stats)
    {
        return stats.MaxBy(x => x.Season);
    }
    
    public static int CurrentSeasonNumber(this IEnumerable<Player> players)
    {
        return players.Select(x => x.Stats.CurrentSeason()).MaxBy(x => x?.Season ?? 0).Season;
    }
    
    public static Player[] CurrentSeason(this IEnumerable<Player> players)
    {
        var season = players.CurrentSeasonNumber();
        return players.Where(x => x.Stats.CurrentSeason()?.Season == season).ToArray();
    }
}