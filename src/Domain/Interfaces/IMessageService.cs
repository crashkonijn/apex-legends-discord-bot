using Domain.Models;

namespace Domain.Interfaces;

public interface IMessageService
{
    Task RankUpdate(Player player, Stat loaded, Stat? existing);
    Task NewSeason(Season currentSeason, Season nextSeason);
    Task NewSplit(Season currentSeason, Season nextSeason);
}