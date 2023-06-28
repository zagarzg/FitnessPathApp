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
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                columns: new[] { "Id", "ExerciseType", "ImageUrl", "IsFavorite", "Name" },
                values: new object[,]
                {
                    { new Guid("032c5827-77d7-4f38-a205-2aea3a215005"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2021/11/dumbbell-romanian-deadlift.gif", true, "Dumbell Romanian Deadlift" },
                    { new Guid("0b9a334e-0849-4a54-91bd-e6789bcab11c"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/08/barbell-bent-over-row.gif", true, "Barbell Row" },
                    { new Guid("0c12a6c5-d47e-431c-91b0-8e8d9a6fa83a"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/08/cable-seated-row.gif", true, "Cable Row" },
                    { new Guid("18522a6b-c3c0-454e-bc25-d145fb69d0ad"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/05/decline-sit-up.gif", true, "Decline Situp" },
                    { new Guid("1b5889fa-484c-4a55-8df7-7b38415762c2"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/03/dumbbell-lateral-raise.gif", true, "Dumbell Lateral Raise" },
                    { new Guid("1c4e0b57-e66a-4604-960b-122c396b1574"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/04/weighted-dips.gif", true, "Weighted Dips" },
                    { new Guid("1cea4155-1b12-4fec-9d31-1c91c3f6fb60"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/10/dumbbell-shoulder-press.gif", true, "Dumbell Shoulder Press" },
                    { new Guid("23af1cfe-de80-4a25-98f3-9de67f64cf36"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/03/ez-bar-preacher-curl.gif", true, "Ez Preacher Curl" },
                    { new Guid("2607d5fe-bcb6-48e7-a2ed-6ff0693f2361"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/09/barbell-hip-thrust.gif", true, "Hipthrust" },
                    { new Guid("2e1c74ce-d121-4832-8530-d045b717c4e7"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/08/wide-grip-lat-pulldown.gif", true, "Lat Pulldown" },
                    { new Guid("3e165f3d-f9fa-4e27-a5b9-05d3b938d8eb"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/04/barbell-bench-press.gif", true, "Bench Press" },
                    { new Guid("44eb78e8-1353-41e2-ba3d-0b93c4c66af5"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/03/dumbbell-concentration-curl.gif", true, "Dumbell Concentration Curl" },
                    { new Guid("46102532-02a6-4e62-b35b-bcea221b77c1"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/01/plank-movement.gif", true, "Plank" },
                    { new Guid("4e24fd67-6d30-433b-a604-d01ad90ef54e"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/05/incline-barbell-bench-press.gif", true, "Incline Bench Press" },
                    { new Guid("4e47994a-3b58-47b6-b4ac-424a6686cd36"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/08/flat-bench-skull-crusher.gif", true, "Skullcrusher" },
                    { new Guid("4f6cfebb-012b-45df-a565-ae3939d74d26"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2021/08/diamond-push-up.gif", true, "Diamond Pushup" },
                    { new Guid("5020ad9b-2a1f-4634-99a6-f89b65b46ab4"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/11/straight-leg-deadlift.gif", true, "Stiff Leg Deadlift" },
                    { new Guid("5c9d00b3-8095-4a75-8d83-98d5132d4db3"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/01/close-grip-bench-press-movement.gif", true, "Close Grip Bench Press" },
                    { new Guid("60669c8a-df74-4e95-a9ce-c9b7e0be40ab"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/01/dumbbell-lunges.gif", true, "Lunges" },
                    { new Guid("7b548a27-ecdf-4f15-b485-8f57e456d3f5"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/09/chin-ups.gif", true, "Chinup" },
                    { new Guid("828ac5f9-1936-4194-a462-e83163f12162"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/05/barbell-curl.gif", true, "Barbell Curl" },
                    { new Guid("82f28e0d-b7cf-4c9a-98b9-9b757b31212c"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/09/dumbbell-chest-press.gif", true, "Dumbell Chest Press" },
                    { new Guid("846df523-dc1d-4206-917c-7b289e156ccc"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/01/chest-dip-movement.gif", true, "Dips" },
                    { new Guid("8ef4878b-2aa6-4319-bd84-abda5148fba2"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/11/pull-up.gif", true, "Pullup" },
                    { new Guid("9e78421c-e5f8-4040-a0fd-8a12454b03bc"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2023/02/ez-bar-bicep-curl.gif", true, "Ez Bar Curl" },
                    { new Guid("a34f4863-35ee-4baf-86c8-d2a678bc1776"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/06/hanging-leg-raise-movement.gif", true, "Hanging Leg Raises" },
                    { new Guid("a847047f-4af5-444e-a81e-ebacd4097aae"), 1, "https://fitnessprogramer.com/wp-content/uploads/2021/05/Dumbbell-Bulgarian-Split-Squat.gif", true, "Bulgarian Split Squat" },
                    { new Guid("a9bfc5a4-8666-47f0-ab14-929345675627"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/10/incline-dumbbell-curl.gif", true, "Incline Dumbell Curl" },
                    { new Guid("bde86a6d-9a37-420d-93ca-60d7b02dc7c4"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/03/barbell-full-squat.gif", true, "Squat" },
                    { new Guid("be5d2cee-e898-4b3a-8386-ea39fda3622e"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/09/dumbbell-incline-chest-press.gif", true, "Dumbell Incline Press" },
                    { new Guid("c1258b52-4e63-4aa7-a075-a16f4a460357"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/08/dumbbell-chest-fly-muscles.gif", true, "Dumbell Chest Fly" },
                    { new Guid("c1769344-dd80-41fe-bbfc-a16df832929b"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/02/barbell-deadlift-movement.gif", true, "Deadlift" },
                    { new Guid("c328e92b-33ff-4863-9397-d53424ca3f5c"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/04/military-press.gif", true, "Overhead Press" },
                    { new Guid("c3449838-8c95-400a-823f-f06e49618269"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2021/10/cable-tricep-pushdown.gif", true, "Cable Triceps Pushdown" },
                    { new Guid("d6350d47-6274-47be-a43c-ecdf29cf7390"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/03/tricep-overhead-extensions.gif", true, "Triceps Overhead Extension" },
                    { new Guid("dcc607b0-0573-4ba5-b00f-7db3525a8e04"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/03/dumbbell-bicep-curl.gif", true, "Dumbell Bicep Curl" },
                    { new Guid("dd4d5068-0311-4189-b7a4-237bf43306fb"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/10/barbell-reverse-curl.gif", true, "Barbell Reverse Curl" },
                    { new Guid("dd5a8953-4e8b-48c1-973a-741faaece2f7"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2023/03/neutral-grip-pull-ups-shoulder-width.gif", true, "Neutral Grip Pullup" },
                    { new Guid("e006abf7-da50-4530-a93d-8683fed79d43"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/11/push-up.gif", true, "Pushup" },
                    { new Guid("e86748e9-9203-41a2-bdf3-12c9a0b24560"), 1, "https://www.inspireusafoundation.org/wp-content/uploads/2022/06/barbell-romanian-deadlift-movement.gif", true, "Romanian Deadlift" },
                    { new Guid("f856276a-e2ab-4158-bf50-0075197b7a16"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2021/10/single-arm-dumbbell-row.gif", true, "One Arm Dumbell Row" },
                    { new Guid("f8e9dcb1-3269-4b40-8a91-b5cc6c18211e"), 2, "https://www.inspireusafoundation.org/wp-content/uploads/2022/04/dumbbell-hammer-curl.gif", true, "Dumbell Hammer Curl" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Username" },
                values: new object[] { new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"), "admin@hotmail.com", "$2b$10$UDdxSGgMvqr.4oKUSsx.O.lDqO4whg8zEfocSTf/DdhNsqU9VlV2q", "admin" });

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
