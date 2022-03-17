using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessPathApp.DomainLayer.Migrations
{
    public partial class DefineUserAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"),
                column: "Password",
                value: "$2b$10$sZciQJmbX9ABJHVGpuUpaufNsMwo7G5GrKrz3uS33.lvdFpeX2AUa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"),
                column: "Password",
                value: "$2b$10$uSC7UwAon9I58xkhmM0.I.4oNNjtgDAtHrInKH7YXsfs.cKAQlfCy");
        }
    }
}
