using Botlag.Models;
using Microsoft.EntityFrameworkCore;

namespace Botlag.Database.Models
{
    [Index(nameof(Guesser), nameof(Boss), nameof(ChannelId), IsUnique = true)]
    public class Guess
    {
        public int Id { get; set; }
        public required string Guesser { get; set; }
        public required string Boss { get; set; }
        public required string ItemName { get; set; }
        // public required Item Item { get; set; }
        public ulong ChannelId { get; set; }
    }
}
