using Application.UseCases.GetRanking;
using Application.UseCases.GetStats;
using Application.UseCases.LoadStats;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StatController
{
    private readonly GetStatsUseCase _getStatsUseCase;
    private readonly LoadStatsUseCase _loadStatsUseCase;

    public StatController(GetStatsUseCase getStatsUseCase, LoadStatsUseCase loadStatsUseCase)
    {
        this._getStatsUseCase = getStatsUseCase;
        this._loadStatsUseCase = loadStatsUseCase;
    }
    
    [HttpGet(Name = "GetStats")]
    [ProducesResponseType(typeof(GetStatResponse), StatusCodes.Status200OK)]
    public async Task<IResult> GetStats([FromQuery] GetStatRequest request)
    {
        var result = await this._getStatsUseCase.Execute(new GetStatsInput
        {
            Username = request.Username
        });
        
        return Results.Ok(new GetStatResponse
        {
            Stat = result.Stat
        });
    }
    
    [HttpPost(Name = "LoadStats")]
    [ProducesResponseType(typeof(GetStatResponse), StatusCodes.Status200OK)]
    public async Task<IResult> LoadStats([FromQuery] LoadStatRequest request)
    {
        var result = await this._loadStatsUseCase.Execute(new LoadStatsInput
        {
            Username = request.Username
        });
        
        return Results.Ok(new LoadStatResponse
        {
            Stat = result.Stat
        });
    }
}

public class GetStatRequest
{
    public string Username { get; set; }
}
public class GetStatResponse
{
    public Stat? Stat { get; set; }
}
public class LoadStatRequest
{
    public string Username { get; set; }
}
public class LoadStatResponse
{
    public Stat? Stat { get; set; }
}