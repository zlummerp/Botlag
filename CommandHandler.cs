using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

namespace Botlag
{
    public class CommandHandler
    {
        #region Fields

        private readonly IServiceProvider _services;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;

        #endregion

        #region Constructor

        public CommandHandler(IServiceProvider services, DiscordSocketClient client, CommandService commandService)
        {
            _services = services;
            _client = client;
            _commandService = commandService;
        }

        #endregion

        #region Public Functions

        public async Task InitializeAsync()
        {
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            _commandService.Log += LogAsync;
            _client.MessageReceived += HandleMessageAsync;
        }

        #endregion

        #region Private Functions

        private async Task HandleMessageAsync(SocketMessage message)
        {
            var argPosition = 0;

            if (message.Author.IsBot)
            {
                return;
            }

            var userMessage = (SocketUserMessage)message;
            if (userMessage == null)
            {
                return;
            }

            if (!userMessage.HasMentionPrefix(_client.CurrentUser, ref argPosition))
            {
                return;
            }

            var context = new SocketCommandContext(_client, userMessage);
            await _commandService.ExecuteAsync(context, argPosition, _services);
        }

        private Task LogAsync(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.Message);
            return Task.CompletedTask;
        }

        #endregion
    }
}
