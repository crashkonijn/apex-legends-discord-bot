using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly IDbContextFactory<StatsContext> _contextFactory;
    private readonly IMapper _mapper;

    public PlayerRepository(IDbContextFactory<StatsContext> contextFactory, IMapper mapper)
    {
        this._contextFactory = contextFactory;
        this._mapper = mapper;
    }

    public async Task<Player> GetOrCreate(Platform platform, string username)
    {
        var context = await this._contextFactory.CreateDbContextAsync();

        var existing = await context.Players.Where(x => x.Platform == platform && x.Username == username).FirstOrDefaultAsync();

        if (existing is not null)
            return this._mapper.Map<Player>(existing);;
        
        existing = new Entities.Player
        {
            Platform = platform,
            Username = username
        };
        context.Players.Add(existing);
        await context.SaveChangesAsync();
        
        return this._mapper.Map<Player>(existing);
    }

    public async Task Upsert(Player player)
    {
        var context = await this._contextFactory.CreateDbContextAsync();

        var existing = await context.Players.Where(x => x.Username == player.Username).FirstOrDefaultAsync();

        if (existing is null)
        {
            existing = new Entities.Player();
            this._mapper.Map(player, existing);
            context.Players.Add(existing);
        }
        else
        {
            this._mapper.Map(player, existing);
        }

        await context.SaveChangesAsync();
    }

    public async Task<Player[]> GetTracked()
    {
        var context = await this._contextFactory.CreateDbContextAsync();

        var players = await context.Players.Where(x => x.IsTracking).Include(x => x.Stats).ToArrayAsync();

        return this._mapper.Map<Player[]>(players);
    }

    public async Task<Player> GetByStat(Stat stat)
    {
        var context = await this._contextFactory.CreateDbContextAsync();

        var player = await context.Players.Where(x => x.Username == stat.Username)
            .Include(x => x.Stats)
            .Include(x => x.User)
            .FirstOrDefaultAsync();

        return this._mapper.Map<Player>(player);
    }

    public async Task<Season> GuessSeason()
    {
        var players = await this.GetTracked();
        var seasons = players.SelectMany(x => x.Stats.Select(y => new Season
        {
            Number = y.Season,
            Split = y.Split
        }));
        
        var guess= seasons.MaxBy(x => x.Id);
        
        return guess ?? new Season();
    }
}