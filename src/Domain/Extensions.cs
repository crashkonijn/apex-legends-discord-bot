using Domain.Models;

namespace Domain;

public static class Extensions
{
    public static Stat? CurrentSeason(this IEnumerable<Stat> stats)
    {
        return stats.MaxBy(x => x.Season);
    }

    public static Player[] CurrentSeason(this Player[] players, Season season)
    {
        return players.Where(x => x.Stats.Any(y => y.Season == season.Number && y.Split == season.Split)).ToArray();
    }
}