using Discord.Commands;
using Botlag.Constants.BossInfo;
using Botlag.Database.Controllers;

namespace Botlag.Commands
{
    public class CheckGuess : ModuleBase<SocketCommandContext>
    {
        #region Constants

        private const string genericCommand = "Check";

        #endregion

        #region Commands

        [Command(genericCommand, true)]
        public async Task GenericCheck()
        {
            var username = Context.User.Username;
            var channelId = Context.Message.Channel.Id;
            var commandMessageIndex = Context.Message.Content.IndexOf(genericCommand, StringComparison.InvariantCultureIgnoreCase);
            var bossText = Context.Message.Content[(commandMessageIndex + genericCommand.Length)..].Trim();

            var bossName = BossNames.CheckBossNameText(bossText);

            var guess = GuessController.GetGuess(username, bossName, channelId);
            if (guess == null)
            {
                await ReplyAsync($"No drop found for user: {username}");
                return;
            }

            await ReplyAsync($"{username} has called: {guess.ItemName}");
        }

        #endregion
    }
}
