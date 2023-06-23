using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPathApp.DomainLayer.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExerciseChoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExerciseType = table.Column<int>(type: "int", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseChoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeightLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "FoodItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Carbs = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<int>(type: "int", nullable: false),
                    Fat = table.Column<int>(type: "int", nullable: false),
                    FoodLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodItems_FoodLogs_FoodLogId",
                        column: x => x.FoodLogId,
                        principalTable: "FoodLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    TrainingLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseChoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_ExerciseChoices_ExerciseChoiceId",
                        column: x => x.ExerciseChoiceId,
                        principalTable: "ExerciseChoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Exercises_TrainingLogs_TrainingLogId",
                        column: x => x.TrainingLogId,
                        principalTable: "TrainingLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExerciseChoices",
                columns: new[] { "Id", "ExerciseType", "ImageData", "IsFavorite", "Name" },
                values: new object[,]
                {
                    { new Guid("3e165f3d-f9fa-4e27-a5b9-05d3b938d8eb"), 0, null, true, "Bench Press" },
                    { new Guid("bde86a6d-9a37-420d-93ca-60d7b02dc7c4"), 0, null, true, "Squat" },
                    { new Guid("c1769344-dd80-41fe-bbfc-a16df832929b"), 0, null, true, "Deadlift" },
                    { new Guid("c328e92b-33ff-4863-9397-d53424ca3f5c"), 0, null, true, "Overhead Press" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Username" },
                values: new object[] { new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), "admin@hotmail.com", "$2b$10$5.eynkHdHLVnxUx34/VK4u3ejx6pQlhpk8iRhW4zd7rDxChhYaYUi", "admin" });

            migrationBuilder.InsertData(
                table: "FoodLogs",
                columns: new[] { "Id", "Date", "UserId" },
                values: new object[] { new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"), new DateTime(2022, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2") });

            migrationBuilder.InsertData(
                table: "TrainingLogs",
                columns: new[] { "Id", "Date", "UserId" },
                values: new object[] { new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), new DateTime(2022, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2") });

            migrationBuilder.InsertData(
                table: "WeightLogs",
                columns: new[] { "Id", "Date", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559"), new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), 77.5 },
                    { new Guid("52fce968-8a43-4dbd-ab26-dcf334c149dd"), new DateTime(2023, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), 77.799999999999997 },
                    { new Guid("8514a58d-0edc-46bc-a0c8-bd912b5f5742"), new DateTime(2023, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), 77.599999999999994 },
                    { new Guid("9d9926f0-ee0b-4cdb-b4bd-c178377d8868"), new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), 77.299999999999997 },
                    { new Guid("c4714153-23fc-4413-b6dc-7fa230cb8883"), new DateTime(2023, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), 78.0 }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "ExerciseChoiceId", "Reps", "Sets", "TrainingLogId", "Weight" },
                values: new object[,]
                {
                    { new Guid("46aa1ca5-4670-4a38-a840-96204dd0b3a2"), new Guid("bde86a6d-9a37-420d-93ca-60d7b02dc7c4"), 5, 5, new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), 140.0 },
                    { new Guid("82a61b04-1cda-4045-abb5-0c1596f9aa36"), new Guid("3e165f3d-f9fa-4e27-a5b9-05d3b938d8eb"), 5, 5, new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), 100.0 },
                    { new Guid("853172d8-26ca-4de1-acc0-b28d753c328f"), new Guid("c1769344-dd80-41fe-bbfc-a16df832929b"), 5, 3, new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), 180.0 },
                    { new Guid("9d9d6825-98ba-47cf-b137-2f6431a047ca"), new Guid("c328e92b-33ff-4863-9397-d53424ca3f5c"), 8, 3, new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), 60.0 },
                    { new Guid("e937cea5-99db-4068-96a2-c75fde51df74"), new Guid("c328e92b-33ff-4863-9397-d53424ca3f5c"), 8, 3, new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), 70.0 }
                });

            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "Id", "Calories", "Carbs", "Fat", "FoodLogId", "Name", "Protein" },
                values: new object[,]
                {
                    { new Guid("81b43909-4490-42fe-af1d-f0e952cf727a"), 325, 25, 3, new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"), "Zbregov Protein", 50 },
                    { new Guid("aac86f05-0ed4-43a1-876f-6ba34f605661"), 170, 0, 7, new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"), "Tuna", 26 },
                    { new Guid("fe8e839e-c834-4b88-ab3c-e8fe0610a3f1"), 356, 72, 2, new Guid("da789a67-5481-4ac9-b338-0a9b3c069a1f"), "Pasta", 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_ExerciseChoiceId",
                table: "Exercises",
                column: "ExerciseChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_TrainingLogId",
                table: "Exercises",
                column: "TrainingLogId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_FoodLogId",
                table: "FoodItems",
                column: "FoodLogId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodLogs_UserId",
                table: "FoodLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingLogs_UserId",
                table: "TrainingLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightLogs_UserId",
                table: "WeightLogs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "WeightLogs");

            migrationBuilder.DropTable(
                name: "ExerciseChoices");

            migrationBuilder.DropTable(
                name: "TrainingLogs");

            migrationBuilder.DropTable(
                name: "FoodLogs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
