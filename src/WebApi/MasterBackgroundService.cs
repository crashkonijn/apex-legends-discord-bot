using Application.Services;
using Discord;

namespace WebApi;

public class MasterBackgroundService : IHostedService
{
    private readonly BotBackgroundService _botBackgroundService;
    private readonly StatsBackgroundService _statsBackgroundService;

    public MasterBackgroundService(BotBackgroundService botBackgroundService, StatsBackgroundService statsBackgroundService)
    {
        this._botBackgroundService = botBackgroundService;
        this._statsBackgroundService = statsBackgroundService;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await this._botBackgroundService.StartAsync(cancellationToken);
        await this._statsBackgroundService.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await this._botBackgroundService.StopAsync(cancellationToken);
        await this._statsBackgroundService.StopAsync(cancellationToken);
    }
}