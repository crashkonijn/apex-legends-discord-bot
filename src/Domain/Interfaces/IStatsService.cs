using Domain.Models;

namespace Domain.Interfaces;

public interface IStatsService
{
    Task<Stat?> GetStats(string username);
    Task<Stat?> LoadAndStore(string username);
    Task LoadAndCompare(string username);
    Task<Stat[]> GetAll();
}