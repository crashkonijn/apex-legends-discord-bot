
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class SeasonRepository : ISeasonRepository
{
    private readonly IDbContextFactory<StatsContext> _contextFactory;
    private readonly IMapper _mapper;
    private Season? _season;

    public SeasonRepository(IDbContextFactory<StatsContext> contextFactory, IMapper mapper)
    {
        this._contextFactory = contextFactory;
        this._mapper = mapper;
    }

    public async Task<bool> HasSeason()
    {
        var context = await this._contextFactory.CreateDbContextAsync();
        
        return await context.Seasons.AnyAsync();
    }

    public async Task<Season> GetCurrentSeason()
    {
        if (this._season is not null)
            return this._season;
        
        var context = await this._contextFactory.CreateDbContextAsync();
        var season = await context.Seasons.OrderByDescending(x => x.Id).FirstAsync();

        var modelSeason = this._mapper.Map<Season>(season);
        this._season = modelSeason;

        return modelSeason;
    }

    public async Task<Season> NewSeasonStart(int number, int split = 0)
    {
        Console.WriteLine($"New season started: {number}-{split}");
        
        var context = await this._contextFactory.CreateDbContextAsync();
        
        var season = new Entities.Season
        {
            Number = number,
            Split = split
        };

       context.Seasons.Add(season);
       await context.SaveChangesAsync();

       this._season = null;

       return this._mapper.Map<Season>(season);
    }

    public async Task<Season> SeasonSplit()
    {
        Console.WriteLine("New season split");
        
        var context = await this._contextFactory.CreateDbContextAsync();
        
        var current = await this.GetCurrentSeason();
        var season = new Entities.Season
        {
            Number = current.Number,
            Split = current.Split + 1
        };

        context.Seasons.Add(season);
        await context.SaveChangesAsync();
        
        this._season = null;

        return this._mapper.Map<Season>(season);
    }
}