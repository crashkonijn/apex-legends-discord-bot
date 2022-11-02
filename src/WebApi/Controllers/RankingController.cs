using Application.UseCases.GetRanking;
using Application.UseCases.GetStats;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RankingController
{
    private readonly GetRankingUseCase _getRankingUseCase;

    public RankingController(GetRankingUseCase getRankingUseCase)
    {
        this._getRankingUseCase = getRankingUseCase;
    }

    [HttpGet(Name = "GetRanking")]
    [ProducesResponseType(typeof(GetRankingResponse), StatusCodes.Status200OK)]
    public async Task<IResult> GetRanking([FromQuery] GetRankingRequest request)
    {
        var result = await this._getRankingUseCase.Execute(new GetRankingInput());
        
        return Results.Ok(new GetRankingResponse
        {
            Season = result.Season,
            Players = result.Players,
        });
    }
}

public class GetRankingRequest {}
public class GetRankingResponse
{
    public int Season { get; set; }
    public Player[] Players { get; set; }
}