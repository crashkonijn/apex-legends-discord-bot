using AutoMapper;
using Database.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Stat = Domain.Models.Stat;

namespace Database.Repositories;

public class StatsRepository : IStatsRepository
{
    private readonly IDbContextFactory<StatsContext> _contextFactory;
    private readonly IMapper _mapper;
    private readonly IPlayerRepository _playerRepository;

    public StatsRepository(IDbContextFactory<StatsContext> contextFactory, IMapper mapper, IPlayerRepository playerRepository)
    {
        this._contextFactory = contextFactory;
        this._mapper = mapper;
        this._playerRepository = playerRepository;
    }
    
    public async Task<Stat?> GetStats(string username, Platform platform = Platform.Origin)
    {
        var context = await this._contextFactory.CreateDbContextAsync();

        var stats = await context.Players.Where(x => x.Platform == platform && x.Username == username).Select(x => x.Stats).FirstOrDefaultAsync();
        var stat = stats?.MaxBy(x => x.Season);

        if (stat is null)
            return null;

        stat.Player = new Player
        {
            Username = username
        };

        return this._mapper.Map<Stat>(stat);
    }

    public async Task Upsert(Stat stat)
    {
        var context = await this._contextFactory.CreateDbContextAsync();

        var player = await this._playerRepository.GetOrCreate(Platform.Origin, stat.Username);

        var existing = await context.Stats.Where(x => x.PlayerId == player.Id && x.Season == stat.Season).FirstOrDefaultAsync();

        if (existing is null)
        {
            existing = new Entities.Stat
            {
                PlayerId = player.Id
            };
            this._mapper.Map(stat, existing);
            context.Stats.Add(existing);
        }
        else
        {
            this._mapper.Map(stat, existing);
        }

        await context.SaveChangesAsync();
    }

    public async Task<Stat[]> GetAll()
    {
        var context = await this._contextFactory.CreateDbContextAsync();

        var entities = await context.Stats.Include(x => x.Player).ToArrayAsync();

        return this._mapper.Map<Stat[]>(entities);
    }
}