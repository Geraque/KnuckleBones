using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication101.Migrations
{
    public partial class LobbyTypesAndNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lobbyName",
                table: "lobby",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "lobby",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "lobby",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lobbyName",
                table: "lobby");

            migrationBuilder.DropColumn(
                name: "password",
                table: "lobby");

            migrationBuilder.DropColumn(
                name: "type",
                table: "lobby");
        }
    }
}
