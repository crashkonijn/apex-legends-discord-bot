using Discord.Commands.Support;
using Discord.WebSocket;
using Domain.Interfaces;

namespace Discord.Commands;

public class TrackedCommand : SlashCommandBase
{
    private readonly IUserRepository _userRepository;
    public override string Name => "tracked";

    public TrackedCommand(IUserRepository userRepository)
    {
        this._userRepository = userRepository;
    }
    
    public override SlashCommandBuilder Init()
    {
        return this.GetBuilder()
            .WithDescription("Get currently tracked players");
    }

    public override async Task Handle(SocketSlashCommand command)
    {
        var user = await this._userRepository.GetByDiscordId(command.User.Id);

        if (user is null)
        {
            Console.WriteLine("User null");
            await command.RespondAsync($"No players found for {command.User.Id}");
            return;
        }

        Console.WriteLine("User found!");
        await command.RespondAsync(string.Join("\n", user.Players.Select(x => $"{x.Username} ({x.Platform.ToString()})")));
    }
}