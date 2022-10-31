using Application.Services;
using Application.UseCases.Import;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static void RegisterApplicationModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IStatsService, StatsService>();

        services.AddSingleton<IImportUseCase, ImportUseCase>();
        
        services.Configure<ApplicationOptions>(config.GetSection(ApplicationOptions.ConfigurationEntry));
    }
}