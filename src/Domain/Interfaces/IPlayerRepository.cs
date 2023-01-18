using Domain.Enums;
using Domain.Models;

namespace Domain.Interfaces;

public interface IPlayerRepository
{
    Task<Player> GetOrCreate(Platform platform, string username);
    Task Upsert(Player player);
    Task<Player[]> GetTracked();
    Task<Player> GetByStat(Stat stat);
    Task<Season> GuessSeason();
}