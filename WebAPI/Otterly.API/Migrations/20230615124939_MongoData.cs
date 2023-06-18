using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otterly.API.Migrations
{
    /// <inheritdoc />
    public partial class MongoData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TwitchID",
                table: "OtterlyAppsUsers",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BingoSlots_CardID",
                table: "BingoSlots",
                column: "CardID");

            migrationBuilder.AddForeignKey(
                name: "FK_BingoSlots_BingoCards_CardID",
                table: "BingoSlots",
                column: "CardID",
                principalTable: "BingoCards",
                principalColumn: "CardID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BingoSlots_BingoCards_CardID",
                table: "BingoSlots");

            migrationBuilder.DropIndex(
                name: "IX_BingoSlots_CardID",
                table: "BingoSlots");

            migrationBuilder.DropColumn(
                name: "TwitchID",
                table: "OtterlyAppsUsers");
        }
    }
}
