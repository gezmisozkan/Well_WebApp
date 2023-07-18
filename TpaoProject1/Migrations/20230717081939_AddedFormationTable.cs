using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpaoProject1.Migrations
{
    /// <inheritdoc />
    public partial class AddedFormationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Formation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    wellid = table.Column<int>(type: "int", nullable: false),
                    Form_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Form_meter = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formation", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Formation");
        }
    }
}
