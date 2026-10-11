using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Botlag.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guesser = table.Column<string>(type: "TEXT", nullable: false),
                    Boss = table.Column<string>(type: "TEXT", nullable: false),
                    ItemName = table.Column<string>(type: "TEXT", nullable: false),
                    ChannelId = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guesses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guesses_Guesser_Boss_ChannelId",
                table: "Guesses",
                columns: new[] { "Guesser", "Boss", "ChannelId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guesses");
        }
    }
}
