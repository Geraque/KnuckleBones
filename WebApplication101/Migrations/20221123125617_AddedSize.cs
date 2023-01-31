using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication101.Migrations
{
    public partial class AddedSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "lobby",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "game",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "lobby");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "game");
        }
    }
}
