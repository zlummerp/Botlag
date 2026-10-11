using Botlag.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Botlag.Database
{
    public class GuessingContext : DbContext
    {
        public DbSet<Guess> Guesses { get; set; }

        public string DbPath { get; }

        public GuessingContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "dropGuesses.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
