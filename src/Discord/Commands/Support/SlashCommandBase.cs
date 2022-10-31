using Discord.WebSocket;

namespace Discord.Commands.Support;

public abstract class SlashCommandBase : ISlashCommand
{
    public abstract string Name { get; }
    
    protected SlashCommandBuilder GetBuilder()
    {
        return new SlashCommandBuilder()
            .WithName(this.Name);
    }

    public abstract SlashCommandBuilder Init();
    public abstract Task Handle(SocketSlashCommand command);
}