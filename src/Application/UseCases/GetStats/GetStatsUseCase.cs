using Domain.Interfaces;

namespace Application.UseCases.GetStats;

public class GetStatsUseCase
{
    private readonly ITrackerClient _trackerClient;

    public GetStatsUseCase(ITrackerClient trackerClient)
    {
        this._trackerClient = trackerClient;
    }
    
    public async Task<GetStatsOutput> Execute(GetStatsInput input)
    {
        var stat = await this._trackerClient.GetStats(input.Username);

        return new GetStatsOutput
        {
            Stat = stat
        };
    }
}