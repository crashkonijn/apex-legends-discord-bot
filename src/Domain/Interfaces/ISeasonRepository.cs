using Domain.Models;

namespace Domain.Interfaces;

public interface ISeasonRepository
{
    Task<bool> HasSeason();
    Task<Season> GetCurrentSeason();
    Task<Season> NewSeasonStart(int number, int split = 0);
    Task<Season> SeasonSplit();
}