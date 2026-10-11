using Discord.Commands;
using Botlag.Constants.BossInfo;
using Botlag.Constants.ItemLists;
using Botlag.Database.Controllers;
using Botlag.Models;

namespace Botlag.Commands
{
    public class DropGot : ModuleBase<SocketCommandContext>
    {
        #region Constants

        private const string genericCommand = "DropGot";

        #endregion

        #region Commands

        [Command(genericCommand, true)]
        public async Task GenericGot()
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

            var correctGuessers = GuessController.FindCorrectGuessesForBoss(itemCalled, channelId);

            if (correctGuessers.Count > 0)
            {
                await ReplyAsync($"Users: {string.Join(",", correctGuessers)} have correctly called the drop for boss: {boss}");
                return;
            }

            await ReplyAsync($"No users have successfully guessed the item correctly ({itemCalled.Name})");

            await CheckGlobalGuesses(itemCalled, channelId);
        }

        #endregion

        #region Private Helper Methods

        private async Task CheckGlobalGuesses(Item itemCalled, ulong channelId)
        {
            var correctGuessers = GuessController.FindCorrectGuessesForGlobal(itemCalled, channelId);

            if (correctGuessers.Count > 0)
            {
                await ReplyAsync($"Users: {string.Join(",", correctGuessers)} have correctly called the drop for boss: {BossNames.Global}");
                return;
            }

            await ReplyAsync($"No users have successfully guessed the {BossNames.Global} item correctly ({itemCalled.Name})");
        }

        #endregion
    }
}
