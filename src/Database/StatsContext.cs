using Application;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Database;

public class StatsContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Stat> Stats { get; set; }
    public DbSet<Season> Seasons { get; set; }

    public readonly string DbPath;

    
    public StatsContext(IOptions<ApplicationOptions> options)
    {
        Console.WriteLine(options.Value.GetPath("config/stats.db"));
        this.DbPath = options.Value.GetPath("stats.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={this.DbPath}");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<User>()
            .HasMany(x => x.Players)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
        
        modelBuilder.Entity<Player>()
            .HasKey(c => c.Id);
        
        modelBuilder.Entity<Player>()
            .HasMany(x => x.Stats)
            .WithOne(x => x.Player)
            .HasForeignKey(x => x.PlayerId);
        
        modelBuilder.Entity<Stat>()
            .HasKey(c => new { c.Id });
        
        modelBuilder.Entity<Stat>()
            .HasIndex(c => new { c.PlayerId, c.Season, c.Split })
            .IsUnique();
        
        modelBuilder.Entity<Season>()
            .HasKey(c => c.Id);
    }
}