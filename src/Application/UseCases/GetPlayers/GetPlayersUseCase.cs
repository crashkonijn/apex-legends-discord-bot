using Domain.Interfaces;

namespace Application.UseCases.GetPlayers;

public class GetPlayersUseCase
{
    private readonly IPlayerRepository _playerRepository;

    public GetPlayersUseCase(IPlayerRepository playerRepository)
    {
        this._playerRepository = playerRepository;
    }
    
    public async Task<GetPlayersOutput> Execute(GetPlayersInput input)
    {
        var players = await this._playerRepository.GetTracked();

        return new GetPlayersOutput
        {
            Players = players
        };
    }
}