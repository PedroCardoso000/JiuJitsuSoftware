using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrinityJiuJitsu.Infrastructure.Migrations
{
    public partial class RemoveMockedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TrainingClasses",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Gyms",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Trinity IBF" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Alan (Faixa Azul)" });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "CreatedAt", "GymId", "Name" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "Sede Fortaleza" });

            migrationBuilder.InsertData(
                table: "TrainingClasses",
                columns: new[] { "Id", "BranchId", "CreatedAt", "Date", "Name" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 4, 7, 19, 0, 0, 0, DateTimeKind.Utc), "Fundamentos - Segunda 19h" });
        }
    }
}
