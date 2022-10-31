using Discord.WebSocket;

namespace Discord.Commands.Support;

public interface ISlashCommand
{
    string Name { get; }
    SlashCommandBuilder Init();
    Task Handle(SocketSlashCommand command);
}