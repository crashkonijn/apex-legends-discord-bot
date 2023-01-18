using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class StatCompareService : IStatCompareService
{

    public bool IsNewSplit(Stat? existing, Stat newStat)
    {
        if (existing is null)
            return false;

        var change = newStat.Rank - existing.Rank;
        
        if (change < -1000)
            return true;

        return false;
    }

    public bool IsRankUp(Stat? existing, Stat newStat)
    {
        if (existing is null)
            return true;
        
        if (existing.RankName == newStat.RankName)
            return false;

        return newStat.Rank > existing.Rank;
    }

    public bool IsRankDown(Stat? existing, Stat newStat)
    {
        if (existing is null)
            return false;
        
        if (existing.RankName == newStat.RankName)
            return false;

        return newStat.Rank < existing.Rank;
    }

    public bool IsNewSeasonOrSplit(Stat? existing, Stat newStat)
    {
        if (existing is null)
            return true;

        if (existing.Season != newStat.Season)
            return true;

        if (existing.Split != newStat.Split)
            return true;

        return false;
    }
}