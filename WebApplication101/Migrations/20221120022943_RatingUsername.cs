using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication101.Migrations
{
    public partial class RatingUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "rating",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "rating");
        }
    }
}
