using Domain.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Application.Services;

public class StatsBackgroundService : IHostedService
{
    private readonly IStatsService _statsService;
    private readonly IPlayerRepository _playerRepository;

    public StatsBackgroundService(IStatsService statsService, IPlayerRepository playerRepository)
    {
        this._statsService = statsService;
        this._playerRepository = playerRepository;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await this._statsService.LoadAndCompare("CrashKonijn");
        return;
        
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                
                // await this.LoadUsers();
                await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    private async Task LoadUsers()
    {
        foreach (var username in await this.GetUsers())
        {
            await this._statsService.LoadAndCompare(username);
            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }

    private async Task<string[]> GetUsers()
    {
        var rnd = new Random();
        var usernames = (await this._playerRepository.GetTracked()).Where(x => x.IsTracking).Select(x => x.Username);
        return usernames.OrderBy(x => rnd.Next()).ToArray();    
    }

    public async Task StopAsync(CancellationToken cancellationToken) 
    {
        await Task.CompletedTask;
    }
}