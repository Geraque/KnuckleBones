using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication101.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "game",
                columns: table => new
                {
                    idGame = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUser1 = table.Column<long>(type: "bigint", nullable: false),
                    idUser2 = table.Column<long>(type: "bigint", nullable: false),
                    field1 = table.Column<string>(type: "text", nullable: false),
                    field2 = table.Column<string>(type: "text", nullable: false),
                    dice = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game", x => x.idGame);
                });

            migrationBuilder.CreateTable(
                name: "lobby",
                columns: table => new
                {
                    idGame = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUser1 = table.Column<long>(type: "bigint", nullable: false),
                    idUser2 = table.Column<long>(type: "bigint", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lobby", x => x.idGame);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game");

            migrationBuilder.DropTable(
                name: "lobby");
        }
    }
}
