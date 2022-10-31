using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbContextFactory<StatsContext> _contextFactory;
    private readonly IMapper _mapper;

    public UserRepository(IDbContextFactory<StatsContext> contextFactory, IMapper mapper)
    {
        this._contextFactory = contextFactory;
        this._mapper = mapper;
    }
    
    public async Task Upsert(User user)
    {
        var context = await this._contextFactory.CreateDbContextAsync();

        var existing = await context.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();

        if (existing is null)
        {
            existing = new Entities.User();
            this._mapper.Map(user, existing);
            context.Users.Add(existing);
        }
        else
        {
            this._mapper.Map(user, existing);
        }

        await context.SaveChangesAsync();
    }

    public async Task<User?> GetByDiscordId(ulong discordId)
    {
        var context = await this._contextFactory.CreateDbContextAsync();

        var user = await context.Users.Where(x => x.DiscordId == discordId)
            .Include(x => x.Players)
            .FirstOrDefaultAsync();

        if (user is null)
            return null;

        return this._mapper.Map<User>(user);
    }
}