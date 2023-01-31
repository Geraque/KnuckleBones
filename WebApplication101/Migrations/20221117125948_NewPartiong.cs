using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication101.Migrations
{
    public partial class NewPartiong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rating",
                columns: table => new
                {
                    IdUser = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Win = table.Column<int>(type: "integer", nullable: false),
                    Lose = table.Column<int>(type: "integer", nullable: false),
                    Draw = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rating", x => x.IdUser);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rating");
        }
    }
}
