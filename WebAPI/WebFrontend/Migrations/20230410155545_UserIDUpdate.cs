using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebFrontend.Migrations
{
    public partial class UserIDUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "AspNetUsers",
                type: "char(36)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Test",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
