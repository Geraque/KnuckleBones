using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication101.Migrations
{
    public partial class Log : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "log",
                columns: table => new
                {
                    idGame = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUser1 = table.Column<long>(type: "bigint", nullable: false),
                    idUser2 = table.Column<long>(type: "bigint", nullable: false),
                    field1 = table.Column<string>(type: "text", nullable: false),
                    field2 = table.Column<string>(type: "text", nullable: false),
                    dice = table.Column<int>(type: "integer", nullable: false),
                    move = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log", x => x.idGame);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "log");
        }
    }
}
