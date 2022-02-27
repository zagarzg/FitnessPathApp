using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessPathApp.DomainLayer.Migrations
{
    public partial class DefineExerciseAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });

            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "Id", "Name", "Reps", "Sets", "TrainingLogId", "Weight" },
                values: new object[,]
                {
                    { new Guid("82a61b04-1cda-4045-abb5-0c1596f9aa36"), "Bench Press", 5, 5, new Guid("00000000-0000-0000-0000-000000000000"), 100.0 },
                    { new Guid("46aa1ca5-4670-4a38-a840-96204dd0b3a2"), "Squat", 5, 5, new Guid("00000000-0000-0000-0000-000000000000"), 140.0 },
                    { new Guid("853172d8-26ca-4de1-acc0-b28d753c328f"), "Deadlift", 5, 3, new Guid("00000000-0000-0000-0000-000000000000"), 180.0 },
                    { new Guid("9d9d6825-98ba-47cf-b137-2f6431a047ca"), "Overhead Press", 8, 3, new Guid("00000000-0000-0000-0000-000000000000"), 60.0 },
                    { new Guid("e937cea5-99db-4068-96a2-c75fde51df74"), "Barbell Row", 8, 3, new Guid("00000000-0000-0000-0000-000000000000"), 70.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercise");
        }
    }
}
