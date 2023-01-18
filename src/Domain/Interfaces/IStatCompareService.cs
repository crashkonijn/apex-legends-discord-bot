using Domain.Models;

namespace Domain.Interfaces;

public interface IStatCompareService
{
    bool IsNewSplit(Stat? existing, Stat newStat);
    bool IsRankUp(Stat? existing, Stat newStat);
    bool IsRankDown(Stat? existing, Stat newStat);
    bool IsNewSeasonOrSplit(Stat? existing, Stat newStat);
}