using Domain.Models;

namespace Domain.Interfaces;

public interface ITrackerClient
{
    Task<Stat?> GetStats(string username);
}