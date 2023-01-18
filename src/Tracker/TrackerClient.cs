using System.Net;
using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json.Linq;

namespace Tracker;

public class TrackerClient : ITrackerClient
{
    private readonly IHttpClientFactory _clientFactory;

    public TrackerClient(IHttpClientFactory clientFactory)
    {
        this._clientFactory = clientFactory;
    }

    public async Task<Stat?> GetStats(string username)
    {
        try
        {
            var stats = await this.LoadStats(username);

            return await this.Enrich(stats);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }

    private async Task<Stat?> Enrich(Stat? stat)
    {
        if (stat is null)
            return null;

        var client = this._clientFactory.CreateClient("tracker");

        var response = await client.GetAsync($"/v2/apex/standard/profile/origin/{stat.Username}/sessions");

        if (response.StatusCode != HttpStatusCode.OK)
            return null;

        var data = await response.Content.ReadAsStringAsync();
        var jo = JObject.Parse(data);
        
        var session = jo.SelectToken("data.items").First();
        var isActive = session.SelectToken("metadata.isActive.value")?.Value<bool>() ?? false;
        
        if (!isActive)
            return stat;
        
        var match = session.SelectToken("matches").First();

        try
        {
            var rank = match.SelectToken("stats.rankScore.value").Value<int>();

            if (rank > stat.Rank)
                stat.Rank = rank;
        }
        catch (Exception e) { Console.WriteLine(data); Console.WriteLine(e); }

        try
        {
            stat.RankName = match.SelectToken("stats.rankScore.metadata.rankScoreInfo.name").Value<string>();
        }
        catch (Exception e) { Console.WriteLine(data); Console.WriteLine(e); }

        return stat;
    }

    private async Task<Stat?> LoadStats(string username)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        
        var stat = new Stat
        {
            Username = username
        };
        
        var client = this._clientFactory.CreateClient("tracker");

        var response = await client.GetAsync($"/v2/apex/standard/profile/origin/{username}");
        
        if (response.StatusCode != HttpStatusCode.OK)
            return null;
        
        var data = await response.Content.ReadAsStringAsync();

        var jo = JObject.Parse(data);

        try
        {
            stat.Legend = jo.SelectToken("data.metadata.activeLegendName").Value<string>();
        }
        catch (Exception e) { Console.WriteLine(data); Console.WriteLine(e); }

        try
        {
            stat.Season = jo.SelectToken("data.metadata.currentSeason").Value<int>();
        }
        catch (Exception e) { Console.WriteLine(data); Console.WriteLine(e); }

        try
        {
            var overview = jo.SelectToken("data.segments").First(x => x.SelectToken("type").Value<string>() == "overview");

            stat.RankName = overview.SelectToken("stats.rankScore.metadata.rankName").Value<string>();
            stat.Rank = overview.SelectToken("stats.rankScore.value").Value<int>();

            stat.Kills = overview.SelectToken("stats.kills.value").Value<int>();
        }
        catch (Exception e)
        {
            Console.WriteLine(data);
            Console.WriteLine(e);
        }

        return stat;
    }
}