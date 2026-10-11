using Discord.Commands;

namespace Botlag.Commands
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("Help", true)]
        public async Task HelpCommand()
        {
            // TODO: Can this dynamically figure out its own name?
            await ReplyAsync("Coming Soon^tm");
//            await ReplyAsync(
//@"Ping the bot @DropCaller with the command you wish to invoke after it.
//Current supported commands are calling/acquiring for CoX and ToB.
//Example: ""@DropCaller Next {Boss}: {Item}""
//Example: ""@DropCaller {Boss} got: {Item}""
//Example: ""@DropCaller Next CoX: Dragon Claws""
//Example: ""@DropCaller CoX got: Twisted Bow""");
        }
    }
}
