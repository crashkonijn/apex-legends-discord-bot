using Application.Services;
using Application.UseCases.EnsureSeason;
using Application.UseCases.GetPlayers;
using Application.UseCases.GetRanking;
using Application.UseCases.GetStats;
using Application.UseCases.Import;
using Application.UseCases.LoadStats;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static void RegisterApplicationModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IStatsService, StatsService>();
        services.AddSingleton<IStatCompareService, StatCompareService>();
        services.AddSingleton<ISeasonService, SeasonService>();

        services.AddSingleton<IImportUseCase, ImportUseCase>();
        services.AddSingleton<IEnsureSeasonUseCase, EnsureSeasonUseCase>();
        
        services.AddSingleton<GetStatsUseCase>();
        services.AddSingleton<GetRankingUseCase>();
        services.AddSingleton<LoadStatsUseCase>();
        services.AddSingleton<GetPlayersUseCase>();
        
        services.Configure<ApplicationOptions>(config.GetSection(ApplicationOptions.ConfigurationEntry));
    }
}