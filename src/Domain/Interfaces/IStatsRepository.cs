using Domain.Enums;
using Domain.Models;

namespace Domain.Interfaces;

public interface IStatsRepository
{
    Task<Stat?> GetStats(string username, Platform platform = Platform.Origin);
    Task Upsert(Stat stat);
    Task<Stat[]> GetAll();
}