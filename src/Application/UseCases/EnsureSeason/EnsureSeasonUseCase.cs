using Domain.Interfaces;

namespace Application.UseCases.EnsureSeason;

public class EnsureSeasonUseCase : IEnsureSeasonUseCase
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly IPlayerRepository _playerRepository;

    public EnsureSeasonUseCase(IPlayerRepository playerRepository, ISeasonRepository seasonRepository)
    {
        this._seasonRepository = seasonRepository;
        this._playerRepository = playerRepository;
    }
    
    public async Task Execute()
    {
        if (await this._seasonRepository.HasSeason())
            return;
        

        var season = await this._playerRepository.GuessSeason();
        
        Console.WriteLine($"Guessing season: {season.Id}");
        
        await this._seasonRepository.NewSeasonStart(season.Number, season.Split);
    }
}