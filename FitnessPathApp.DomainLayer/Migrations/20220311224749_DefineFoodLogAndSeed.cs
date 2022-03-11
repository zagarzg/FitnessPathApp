using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessPathApp.DomainLayer.Migrations
{
    public partial class DefineFoodLogAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodLog", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FoodLog",
                columns: new[] { "Id", "Date" },
                values: new object[] { new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"), new DateTime(2022, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_FoodItem_FoodLogId",
                table: "FoodItem",
                column: "FoodLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItem_FoodLog_FoodLogId",
                table: "FoodItem",
                column: "FoodLogId",
                principalTable: "FoodLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItem_FoodLog_FoodLogId",
                table: "FoodItem");

            migrationBuilder.DropTable(
                name: "FoodLog");

            migrationBuilder.DropIndex(
                name: "IX_FoodItem_FoodLogId",
                table: "FoodItem");
        }
    }
}
