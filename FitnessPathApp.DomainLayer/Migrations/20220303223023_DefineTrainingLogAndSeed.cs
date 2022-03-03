using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessPathApp.DomainLayer.Migrations
{
    public partial class DefineTrainingLogAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingLog", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TrainingLog",
                columns: new[] { "Id", "Date" },
                values: new object[] { new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"), new DateTime(2022, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: new Guid("46aa1ca5-4670-4a38-a840-96204dd0b3a2"),
                column: "TrainingLogId",
                value: new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"));

            migrationBuilder.UpdateData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: new Guid("82a61b04-1cda-4045-abb5-0c1596f9aa36"),
                column: "TrainingLogId",
                value: new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"));

            migrationBuilder.UpdateData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: new Guid("853172d8-26ca-4de1-acc0-b28d753c328f"),
                column: "TrainingLogId",
                value: new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"));

            migrationBuilder.UpdateData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: new Guid("9d9d6825-98ba-47cf-b137-2f6431a047ca"),
                column: "TrainingLogId",
                value: new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"));

            migrationBuilder.UpdateData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: new Guid("e937cea5-99db-4068-96a2-c75fde51df74"),
                column: "TrainingLogId",
                value: new Guid("cb31d06e-13da-4ba0-a923-5c062399f3a8"));

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TrainingLogId",
                table: "Exercise",
                column: "TrainingLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_TrainingLog_TrainingLogId",
                table: "Exercise",
                column: "TrainingLogId",
                principalTable: "TrainingLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_TrainingLog_TrainingLogId",
                table: "Exercise");

            migrationBuilder.DropTable(
                name: "TrainingLog");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_TrainingLogId",
                table: "Exercise");

            migrationBuilder.UpdateData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: new Guid("46aa1ca5-4670-4a38-a840-96204dd0b3a2"),
                column: "TrainingLogId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: new Guid("82a61b04-1cda-4045-abb5-0c1596f9aa36"),
                column: "TrainingLogId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: new Guid("853172d8-26ca-4de1-acc0-b28d753c328f"),
                column: "TrainingLogId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: new Guid("9d9d6825-98ba-47cf-b137-2f6431a047ca"),
                column: "TrainingLogId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: new Guid("e937cea5-99db-4068-96a2-c75fde51df74"),
                column: "TrainingLogId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
