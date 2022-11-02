using Application.UseCases.GetPlayers;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController
{
    private readonly GetPlayersUseCase _getPlayersUseCase;

    public PlayerController(GetPlayersUseCase getPlayersUseCase)
    {
        this._getPlayersUseCase = getPlayersUseCase;
    }

    [HttpGet(Name = "GetPlayers")]
    [ProducesResponseType(typeof(GetPlayersResponse), StatusCodes.Status200OK)]
    public async Task<IResult> GetPlayers([FromQuery] GetPlayersRequest request)
    {
        var result = await this._getPlayersUseCase.Execute(new GetPlayersInput());
        
        return Results.Ok(new GetPlayersResponse
        {
            Players = result.Players,
        });
    }
}

public class GetPlayersRequest {}
public class GetPlayersResponse
{
    public Player[] Players { get; set; }
}