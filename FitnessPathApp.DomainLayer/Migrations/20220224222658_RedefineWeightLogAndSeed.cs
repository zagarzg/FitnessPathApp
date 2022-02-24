using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessPathApp.DomainLayer.Migrations
{
    public partial class RedefineWeightLogAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "WeightLogs");

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "WeightLogs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "WeightLogs",
                columns: new[] { "Id", "Date", "Value" },
                values: new object[,]
                {
                    { new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559"), new DateTime(2022, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 77.5 },
                    { new Guid("c4714153-23fc-4413-b6dc-7fa230cb8883"), new DateTime(2022, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 78.0 },
                    { new Guid("9d9926f0-ee0b-4cdb-b4bd-c178377d8868"), new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 77.299999999999997 },
                    { new Guid("8514a58d-0edc-46bc-a0c8-bd912b5f5742"), new DateTime(2022, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 77.599999999999994 },
                    { new Guid("52fce968-8a43-4dbd-ab26-dcf334c149dd"), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 77.799999999999997 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WeightLogs",
                keyColumn: "Id",
                keyValue: new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559"));

            migrationBuilder.DeleteData(
                table: "WeightLogs",
                keyColumn: "Id",
                keyValue: new Guid("52fce968-8a43-4dbd-ab26-dcf334c149dd"));

            migrationBuilder.DeleteData(
                table: "WeightLogs",
                keyColumn: "Id",
                keyValue: new Guid("8514a58d-0edc-46bc-a0c8-bd912b5f5742"));

            migrationBuilder.DeleteData(
                table: "WeightLogs",
                keyColumn: "Id",
                keyValue: new Guid("9d9926f0-ee0b-4cdb-b4bd-c178377d8868"));

            migrationBuilder.DeleteData(
                table: "WeightLogs",
                keyColumn: "Id",
                keyValue: new Guid("c4714153-23fc-4413-b6dc-7fa230cb8883"));

            migrationBuilder.DropColumn(
                name: "Value",
                table: "WeightLogs");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WeightLogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
