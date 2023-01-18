using Domain.Models;

namespace Domain.Interfaces;

public interface ISeasonService
{
    Task HandleSeasonChanges(Stat current, Stat? previousStat);
}