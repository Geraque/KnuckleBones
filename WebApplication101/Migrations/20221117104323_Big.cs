using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication101.Migrations
{
    public partial class Big : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "move",
                table: "log",
                newName: "Move");

            migrationBuilder.RenameColumn(
                name: "idUser2",
                table: "log",
                newName: "IdUser2");

            migrationBuilder.RenameColumn(
                name: "idUser1",
                table: "log",
                newName: "IdUser1");

            migrationBuilder.RenameColumn(
                name: "idGame",
                table: "log",
                newName: "IdGame");

            migrationBuilder.RenameColumn(
                name: "field2",
                table: "log",
                newName: "Field2");

            migrationBuilder.RenameColumn(
                name: "field1",
                table: "log",
                newName: "Field1");

            migrationBuilder.RenameColumn(
                name: "dice",
                table: "log",
                newName: "Dice");

            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "log",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "log",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "lobby",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "lobby",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "lobby",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "lobbyName",
                table: "lobby",
                newName: "LobbyName");

            migrationBuilder.RenameColumn(
                name: "idUser2",
                table: "lobby",
                newName: "IdUser2");

            migrationBuilder.RenameColumn(
                name: "idUser1",
                table: "lobby",
                newName: "IdUser1");

            migrationBuilder.RenameColumn(
                name: "idGame",
                table: "lobby",
                newName: "IdGame");

            migrationBuilder.RenameColumn(
                name: "idUser2",
                table: "game",
                newName: "IdUser2");

            migrationBuilder.RenameColumn(
                name: "idUser1",
                table: "game",
                newName: "IdUser1");

            migrationBuilder.RenameColumn(
                name: "field2",
                table: "game",
                newName: "Field2");

            migrationBuilder.RenameColumn(
                name: "field1",
                table: "game",
                newName: "Field1");

            migrationBuilder.RenameColumn(
                name: "dice",
                table: "game",
                newName: "Dice");

            migrationBuilder.RenameColumn(
                name: "idGame",
                table: "game",
                newName: "IdGame");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Move",
                table: "log",
                newName: "move");

            migrationBuilder.RenameColumn(
                name: "IdUser2",
                table: "log",
                newName: "idUser2");

            migrationBuilder.RenameColumn(
                name: "IdUser1",
                table: "log",
                newName: "idUser1");

            migrationBuilder.RenameColumn(
                name: "IdGame",
                table: "log",
                newName: "idGame");

            migrationBuilder.RenameColumn(
                name: "Field2",
                table: "log",
                newName: "field2");

            migrationBuilder.RenameColumn(
                name: "Field1",
                table: "log",
                newName: "field1");

            migrationBuilder.RenameColumn(
                name: "Dice",
                table: "log",
                newName: "dice");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "log",
                newName: "dateTime");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "log",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "lobby",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "lobby",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "lobby",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "LobbyName",
                table: "lobby",
                newName: "lobbyName");

            migrationBuilder.RenameColumn(
                name: "IdUser2",
                table: "lobby",
                newName: "idUser2");

            migrationBuilder.RenameColumn(
                name: "IdUser1",
                table: "lobby",
                newName: "idUser1");

            migrationBuilder.RenameColumn(
                name: "IdGame",
                table: "lobby",
                newName: "idGame");

            migrationBuilder.RenameColumn(
                name: "IdUser2",
                table: "game",
                newName: "idUser2");

            migrationBuilder.RenameColumn(
                name: "IdUser1",
                table: "game",
                newName: "idUser1");

            migrationBuilder.RenameColumn(
                name: "Field2",
                table: "game",
                newName: "field2");

            migrationBuilder.RenameColumn(
                name: "Field1",
                table: "game",
                newName: "field1");

            migrationBuilder.RenameColumn(
                name: "Dice",
                table: "game",
                newName: "dice");

            migrationBuilder.RenameColumn(
                name: "IdGame",
                table: "game",
                newName: "idGame");
        }
    }
}
