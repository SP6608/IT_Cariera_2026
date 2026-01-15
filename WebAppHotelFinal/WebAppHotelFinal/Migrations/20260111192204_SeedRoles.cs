using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppHotelFinal.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "OnCreated" },
                values: new object[,]
                {
                    { "ROLE_ADMIN", null, "Admin", "ADMIN", new DateTime(2026, 1, 11, 19, 22, 4, 0, DateTimeKind.Utc).AddTicks(2040) },
                    { "ROLE_EMPLOYEE", null, "Employee", "EMPLOYEE", new DateTime(2026, 1, 11, 19, 22, 4, 0, DateTimeKind.Utc).AddTicks(2073) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ROLE_ADMIN");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ROLE_EMPLOYEE");
        }
    }
}
