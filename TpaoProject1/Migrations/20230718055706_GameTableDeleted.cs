using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpaoProject1.Migrations
{
    /// <inheritdoc />
    public partial class GameTableDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Difference = table.Column<int>(type: "int", nullable: false),
                    GuessNumber = table.Column<int>(type: "int", nullable: false),
                    RememberNumber = table.Column<int>(type: "int", nullable: false),
                    Success = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });
        }
    }
}
