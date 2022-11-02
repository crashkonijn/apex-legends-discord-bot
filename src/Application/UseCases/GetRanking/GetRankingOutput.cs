using Domain.Models;

namespace Application.UseCases.GetRanking;

public class GetRankingOutput
{
    public int Season { get; set; }
    public Player[] Players { get; set; }
}