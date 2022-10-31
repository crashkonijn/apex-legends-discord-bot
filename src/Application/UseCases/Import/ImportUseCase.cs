using System.Reflection.Metadata;
using Domain.Interfaces;
using Domain.Models;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Application.UseCases.Import;

public class ImportUseCase : IImportUseCase
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IOptions<ApplicationOptions> _options;

    public ImportUseCase(IPlayerRepository playerRepository, IUserRepository userRepository, IOptions<ApplicationOptions> options)
    {
        this._playerRepository = playerRepository;
        this._userRepository = userRepository;
        this._options = options;
    }

    public async Task Execute()
    {
        var path = this._options.Value.GetPath("import.json");

        try
        {
            var data = await File.ReadAllTextAsync(path);
            var users = JsonSerializer.Deserialize<User[]>(data);
            
            foreach (var user in users ?? Array.Empty<User>())
            {
                await this.SaveUser(user);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    
    private async Task SaveUser(User user)
    {
        await this._userRepository.Upsert(user);

        foreach (var player in user.Players)
        {
            player.UserId = user.Id;
            await this._playerRepository.Upsert(player);
        }
    }
}