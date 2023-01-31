using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication101.Migrations
{
    public partial class dateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "dateTime",
                table: "log",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTime",
                table: "log",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
