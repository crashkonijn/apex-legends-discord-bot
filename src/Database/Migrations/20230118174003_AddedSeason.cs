using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class AddedSeason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stats_PlayerId_Season",
                table: "Stats");

            migrationBuilder.AddColumn<int>(
                name: "Split",
                table: "Stats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Split = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stats_PlayerId_Season_Split",
                table: "Stats",
                columns: new[] { "PlayerId", "Season", "Split" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Stats_PlayerId_Season_Split",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "Split",
                table: "Stats");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_PlayerId_Season",
                table: "Stats",
                columns: new[] { "PlayerId", "Season" },
                unique: true);
        }
    }
}
