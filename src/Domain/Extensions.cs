using Domain.Models;

namespace Domain;

public static class Extensions
{
    public static Stat? CurrentSeason(this IEnumerable<Stat> stats)
    {
        return stats.MaxBy(x => x.Season);
    }
}