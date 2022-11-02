using Domain.Interfaces;

namespace Application.UseCases.LoadStats;

public class LoadStatsUseCase
{
    private readonly IStatsService _statsService;

    public LoadStatsUseCase(IStatsService statsService)
    {
        this._statsService = statsService;
    }
    
    public async Task<LoadStatsOutput> Execute(LoadStatsInput input)
    {
        await this._statsService.LoadAndCompare(input.Username);

        var stats = await this._statsService.GetStats(input.Username);

        return new LoadStatsOutput
        {
            Stat = stats
        };
    }
}