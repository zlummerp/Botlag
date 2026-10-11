using Microsoft.EntityFrameworkCore;

namespace Botlag.Models
{
    [Keyless]
    public class Item
    {
        public required string Name { get; set; }

        public required List<string> Aliases { get; set; }

        public required string Boss { get; set; }
    }
}
