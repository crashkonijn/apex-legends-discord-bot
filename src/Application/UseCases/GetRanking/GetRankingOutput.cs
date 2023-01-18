using Domain.Models;

namespace Application.UseCases.GetRanking;

public class GetRankingOutput
{
    public string Season { get; set; }
    public Player[] Players { get; set; }
}