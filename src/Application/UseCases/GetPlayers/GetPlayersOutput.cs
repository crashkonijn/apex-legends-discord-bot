using Domain.Models;

namespace Application.UseCases.GetPlayers;

public class GetPlayersOutput
{
    public Player[] Players { get; set; }
}