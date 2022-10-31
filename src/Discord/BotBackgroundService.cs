using Discord.Commands;
using Discord.Commands.Support;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Discord;

public class BotBackgroundService : IHostedService
{
    private readonly DiscordSocketClient _client;
    private readonly IEnumerable<ISlashCommand> _commands;
    private readonly DiscordOptions _options;

    public BotBackgroundService(DiscordSocketClient client, IEnumerable<ISlashCommand> commands, IOptions<DiscordOptions> options)
    {
        this._client = client;
        this._commands = commands;
        this._options = options.Value;
        this._client.Log += this.Log;
        this._client.Ready += this.ClientReady;
        this._client.SlashCommandExecuted += this.SlashCommandHandler;
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
    
    private async Task ClientReady()
    {
        try
        {
            await this._client.BulkOverwriteGlobalApplicationCommandsAsync(this._commands.Select(x => x.Init().Build()).ToArray());
        }
        catch(ApplicationCommandException exception)
        {
            Console.WriteLine(exception);
            // // If our command was invalid, we should catch an ApplicationCommandException. This exception contains the path of the error as well as the error message. You can serialize the Error field in the exception to get a visual of where your error is.
            // var json = JsonConvert.SerializeObject(exception.Error, Formatting.Indented);
            //
            // // You can send this error somewhere or just print it to the console, for this example we're just going to print it.
            // Console.WriteLine(json);
        }
    }
    
    private async Task SlashCommandHandler(SocketSlashCommand command)
    {
        Console.WriteLine($"Got command {command.Data.Name}");
        var commandHandler = this._commands.FirstOrDefault(x => x.Name == command.Data.Name);

        if (commandHandler is null)
        {
            Console.WriteLine($"Unknown command: {command.Data.Name}");
            return;
        }

        try
        {
            await commandHandler.Handle(command);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await this._client.LoginAsync(TokenType.Bot, this._options.Token);
        await this._client.StartAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken) => this._client.StopAsync();
}