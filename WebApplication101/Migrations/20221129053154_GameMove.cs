using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication101.Migrations
{
    public partial class GameMove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Move",
                table: "game",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Move",
                table: "game");
        }
    }
}
