using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Botlag
{
    public class Botlag
    {
        #region Constants (Don't Open)

        // TODO: Move the token
        private const string DiscordToken = "";

        #endregion

        #region Fields

        private IServiceProvider _services;
        private DiscordSocketClient _client;
        private CommandService _commandService;

        #endregion

        #region Constructor

        public Botlag(DiscordSocketClient? client = null, CommandService? commandService = null)
        {
            _client = client ?? new DiscordSocketClient(new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                LogLevel = LogSeverity.Debug,
                MessageCacheSize = 50
            });

            _commandService = commandService ?? new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                LogLevel = LogSeverity.Verbose
            });
        }

        #endregion

        #region Public Methods

        public async Task InitializeAsync()
        {
            await _client.LoginAsync(TokenType.Bot, DiscordToken);
            await _client.StartAsync();

            _client.Log += LogAsync;
            _services = SetupServices();

            // Could make it a singleton
            var commandHandler = new CommandHandler(_services, _client, _commandService);
            await commandHandler.InitializeAsync();

            await Task.Delay(-1);
        }

        #endregion

        #region Private Methods

        private Task LogAsync(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.Message);

            return Task.CompletedTask;
        }

        private IServiceProvider SetupServices()
            => new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commandService)
            .BuildServiceProvider();

        #endregion
    }
}
