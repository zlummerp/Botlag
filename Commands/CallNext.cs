using Discord.Commands;
using Botlag.Constants.BossInfo;
using Botlag.Constants.ItemLists;
using Botlag.Database.Controllers;
using Botlag.Database.Models;
using System.Threading.Channels;

namespace Botlag.Commands
{
    public class CallNext : ModuleBase<SocketCommandContext>
    {
        #region Constants

        private const string genericCommand = "Next";

        #endregion

        #region Commands

        [Command(genericCommand, true)]
        public async Task NextGeneric()
        {
            var commandMessageIndex = Context.Message.Content.IndexOf(genericCommand, StringComparison.InvariantCultureIgnoreCase);
            var commandText = Context.Message.Content[(commandMessageIndex + genericCommand.Length)..].Trim();
            var channelId = Context.Message.Channel.Id;

            var commandSplit = commandText.Split(':');
            var bossName = commandSplit[0].Trim();
            var itemText = commandSplit[1].Trim();
            var boss = BossNames.CheckBossNameText(bossName);
            if (string.IsNullOrEmpty(boss))
            {
                await ReplyAsync($"Boss name ({bossName}) not found");
                return;
            }

            var itemCalled = ItemLists.SearchForBossItem(boss, itemText);
            if (itemCalled == null)
            {
                await ReplyAsync("Item not found.");
                return;
            }

            var username = Context.User.Username;

            var guess = GuessController.GetGuess(username, boss, channelId);
            if (guess == null)
            {
                CreateNewGuess(boss, username, itemCalled.Name, channelId);
            }
            else
            {
                if (!GuessController.UpdateDropCall(guess, itemCalled.Name))
                {
                    await ReplyAsync($"Drop update failed. {username} has called: {itemText}");
                    return;
                }
            }

            await ReplyAsync($"Next {(boss == BossNames.Global ? $"{BossNames.Global} " : string.Empty)}drop called. {username} has called: {itemCalled.Name}");
        }

        #endregion

        #region Private Helper Methods

        private void CreateNewGuess(string bossName, string username, string itemCalled, ulong channelId)
        {
            var guess = new Guess()
            {
                Boss = bossName,
                Guesser = username,
                ItemName = itemCalled,
                // Item = itemCalled,
                ChannelId = channelId
            };
            GuessController.SaveNewDropCall(guess);
        }

        #endregion
    }
}
