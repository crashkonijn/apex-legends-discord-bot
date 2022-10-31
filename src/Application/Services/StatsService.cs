using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class StatsService : IStatsService
{
    private readonly IStatsRepository _repository;
    private readonly ITrackerClient _trackerClient;
    private readonly IMessageService _messageService;
    private readonly IPlayerRepository _playerRepository;

    public StatsService(IStatsRepository repository, ITrackerClient trackerClient, IMessageService messageService, IPlayerRepository playerRepository)
    {
        this._repository = repository;
        this._trackerClient = trackerClient;
        this._messageService = messageService;
        this._playerRepository = playerRepository;
    }
    
    public async Task<Stat?> GetStats(string username)
    {
        var existing = await this._repository.GetStats(username);

        if (existing is not null)
        {
            return existing;
        }

        return await this.LoadAndStore(username);
    }

    public async Task<Stat?> LoadAndStore(string username)
    {
        var loaded = await this._trackerClient.GetStats(username);

        await this._repository.Upsert(loaded);

        return loaded;
    }

    public async Task LoadAndCompare(string username)
    {
        Console.WriteLine($"LoadAndCompare: {username}");
        var loaded = await this._trackerClient.GetStats(username);

        if (loaded is null)
            return;
        
        var existing = await this._repository.GetStats(username);
        
        await this._repository.Upsert(loaded);

        if (existing?.RankName == loaded.RankName)
            return;

        var player = await this._playerRepository.GetByStat(loaded);
        
        await this._messageService.RankUpdate(player);
    }

    public Task<Stat[]> GetAll()
    {
        return this._repository.GetAll();
    }
}