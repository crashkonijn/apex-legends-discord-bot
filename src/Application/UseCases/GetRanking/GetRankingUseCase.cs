using Domain;
using Domain.Interfaces;

namespace Application.UseCases.GetRanking;

public class GetRankingUseCase
{
    private readonly IPlayerRepository _playerRepository;
    private readonly ISeasonRepository _seasonRepository;

    public GetRankingUseCase(IPlayerRepository playerRepository, ISeasonRepository seasonRepository)
    {
        this._playerRepository = playerRepository;
        this._seasonRepository = seasonRepository;
    }
    
    public async Task<GetRankingOutput> Execute(GetRankingInput input)
    {
        var season = await this._seasonRepository.GetCurrentSeason();
        var players = await this._playerRepository.GetTracked();
        
        players = players.CurrentSeason(season);

        return new GetRankingOutput
        {
            Season = season.Id,
            Players = players,
        };
    }
}