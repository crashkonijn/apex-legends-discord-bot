using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class SeasonService : ISeasonService
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly IStatCompareService _statCompareService;
    private readonly IMessageService _messageService;

    public SeasonService(ISeasonRepository seasonRepository, IStatCompareService statCompareService, IMessageService messageService)
    {
        this._seasonRepository = seasonRepository;
        this._statCompareService = statCompareService;
        this._messageService = messageService;
    }
    
    public async Task HandleSeasonChanges(Stat current, Stat? previousStat)
    {
        var currentSeason = await this._seasonRepository.GetCurrentSeason();
        var isSameSeason = current.Season == currentSeason.Number;
        var isNewSplit = this._statCompareService.IsNewSplit(previousStat, current);

        if (!isNewSplit && isSameSeason)
        {
            current.Split = currentSeason.Split;
            return;
        }

        if (!isSameSeason)
        {
            var newSeason = await this._seasonRepository.NewSeasonStart(current.Season);
            
            current.Split = newSeason.Split;
            
            await this._messageService.NewSeason(currentSeason, newSeason);
            return;
        }

        if (!isNewSplit)
            return;
        
        var newSplit = await this._seasonRepository.SeasonSplit();
        current.Split = newSplit.Split;
        await this._messageService.NewSplit(currentSeason, newSplit);
    }
}