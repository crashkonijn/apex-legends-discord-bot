using Domain;
using Domain.Interfaces;

namespace Application.UseCases.GetRanking;

public class GetRankingUseCase
{
    private readonly IPlayerRepository _playerRepository;

    public GetRankingUseCase(IPlayerRepository playerRepository)
    {
        this._playerRepository = playerRepository;
    }
    
    public async Task<GetRankingOutput> Execute(GetRankingInput input)
    {
        var players = await this._playerRepository.GetTracked();
        var season = players.CurrentSeasonNumber();
        players = players.CurrentSeason();

        return new GetRankingOutput
        {
            Season = season,
            Players = players,
        };
    }
}