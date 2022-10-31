using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tracker;

public static class TrackerModule
{
    public static void RegisterTrackerModule(this IServiceCollection services, IConfiguration config)
    {
        var section = config.GetSection(TrackerOptions.ConfigurationEntry);
        var options = section.Get<TrackerOptions>();

        services.Configure<TrackerOptions>(section);
        
        services.AddHttpClient("tracker", httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://public-api.tracker.gg");

            httpClient.DefaultRequestHeaders.Add("TRN-Api-Key", options.Token);
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
        });
        
        services.AddSingleton<ITrackerClient, TrackerClient>();
    }
}