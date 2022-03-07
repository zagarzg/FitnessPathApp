using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessPathApp.DomainLayer.Migrations
{
    public partial class DefineFoodItemAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Calories = table.Column<int>(nullable: false),
                    Carbs = table.Column<int>(nullable: false),
                    Protein = table.Column<int>(nullable: false),
                    Fat = table.Column<int>(nullable: false),
                    FoodLogId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItem", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FoodItem",
                columns: new[] { "Id", "Calories", "Carbs", "Fat", "FoodLogId", "Name", "Protein" },
                values: new object[] { new Guid("aac86f05-0ed4-43a1-876f-6ba34f605661"), 170, 0, 7, new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"), "Tuna", 26 });

            migrationBuilder.InsertData(
                table: "FoodItem",
                columns: new[] { "Id", "Calories", "Carbs", "Fat", "FoodLogId", "Name", "Protein" },
                values: new object[] { new Guid("fe8e839e-c834-4b88-ab3c-e8fe0610a3f1"), 356, 72, 2, new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"), "Pasta", 12 });

            migrationBuilder.InsertData(
                table: "FoodItem",
                columns: new[] { "Id", "Calories", "Carbs", "Fat", "FoodLogId", "Name", "Protein" },
                values: new object[] { new Guid("81b43909-4490-42fe-af1d-f0e952cf727a"), 325, 25, 3, new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"), "Zbregov Protein", 50 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItem");
        }
    }
}
