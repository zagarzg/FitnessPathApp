using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessPathApp.DomainLayer.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodLog_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingLog_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeightLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeightLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    table.ForeignKey(
                        name: "FK_FoodItem_FoodLog_FoodLogId",
                        column: x => x.FoodLogId,
                        principalTable: "FoodLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Weight = table.Column<double>(nullable: false),
                    Sets = table.Column<int>(nullable: false),
                    Reps = table.Column<int>(nullable: false),
                    TrainingLogId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_TrainingLog_TrainingLogId",
                        column: x => x.TrainingLogId,
                        principalTable: "TrainingLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Username" },
                values: new object[] { new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), "admin@hotmail.com", "$2b$10$/XFgyxjSodEqSpo6boTO3e.ZRjCZD6PXCulOwijIPTFC1AbBT7kU6", "admin" });

            migrationBuilder.InsertData(
                table: "FoodLog",
                columns: new[] { "Id", "Date", "UserId" },
                values: new object[] { new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"), new DateTime(2022, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2") });

            migrationBuilder.InsertData(
                table: "TrainingLog",
                columns: new[] { "Id", "Date", "UserId" },
                values: new object[] { new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), new DateTime(2022, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2") });

            migrationBuilder.InsertData(
                table: "WeightLogs",
                columns: new[] { "Id", "Date", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559"), new DateTime(2022, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), 77.5 },
                    { new Guid("c4714153-23fc-4413-b6dc-7fa230cb8883"), new DateTime(2022, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), 78.0 },
                    { new Guid("9d9926f0-ee0b-4cdb-b4bd-c178377d8868"), new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), 77.299999999999997 },
                    { new Guid("8514a58d-0edc-46bc-a0c8-bd912b5f5742"), new DateTime(2022, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), 77.599999999999994 },
                    { new Guid("52fce968-8a43-4dbd-ab26-dcf334c149dd"), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), 77.799999999999997 }
                });

            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "Id", "Name", "Reps", "Sets", "TrainingLogId", "Weight" },
                values: new object[,]
                {
                    { new Guid("82a61b04-1cda-4045-abb5-0c1596f9aa36"), "Bench Press", 5, 5, new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), 100.0 },
                    { new Guid("46aa1ca5-4670-4a38-a840-96204dd0b3a2"), "Squat", 5, 5, new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), 140.0 },
                    { new Guid("853172d8-26ca-4de1-acc0-b28d753c328f"), "Deadlift", 5, 3, new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), 180.0 },
                    { new Guid("9d9d6825-98ba-47cf-b137-2f6431a047ca"), "Overhead Press", 8, 3, new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), 60.0 },
                    { new Guid("e937cea5-99db-4068-96a2-c75fde51df74"), "Barbell Row", 8, 3, new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), 70.0 }
                });

            migrationBuilder.InsertData(
                table: "FoodItem",
                columns: new[] { "Id", "Calories", "Carbs", "Fat", "FoodLogId", "Name", "Protein" },
                values: new object[,]
                {
                    { new Guid("aac86f05-0ed4-43a1-876f-6ba34f605661"), 170, 0, 7, new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"), "Tuna", 26 },
                    { new Guid("fe8e839e-c834-4b88-ab3c-e8fe0610a3f1"), 356, 72, 2, new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"), "Pasta", 12 },
                    { new Guid("81b43909-4490-42fe-af1d-f0e952cf727a"), 325, 25, 3, new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"), "Zbregov Protein", 50 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TrainingLogId",
                table: "Exercise",
                column: "TrainingLogId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItem_FoodLogId",
                table: "FoodItem",
                column: "FoodLogId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodLog_UserId",
                table: "FoodLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingLog_UserId",
                table: "TrainingLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightLogs_UserId",
                table: "WeightLogs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "FoodItem");

            migrationBuilder.DropTable(
                name: "WeightLogs");

            migrationBuilder.DropTable(
                name: "TrainingLog");

            migrationBuilder.DropTable(
                name: "FoodLog");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
