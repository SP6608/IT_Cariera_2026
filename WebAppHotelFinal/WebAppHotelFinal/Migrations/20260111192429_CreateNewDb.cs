using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppHotelFinal.Migrations
{
    /// <inheritdoc />
    public partial class CreateNewDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ROLE_ADMIN",
                column: "OnCreated",
                value: new DateTime(2026, 1, 11, 19, 24, 29, 97, DateTimeKind.Utc).AddTicks(2399));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ROLE_EMPLOYEE",
                column: "OnCreated",
                value: new DateTime(2026, 1, 11, 19, 24, 29, 97, DateTimeKind.Utc).AddTicks(2521));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ROLE_ADMIN",
                column: "OnCreated",
                value: new DateTime(2026, 1, 11, 19, 22, 4, 0, DateTimeKind.Utc).AddTicks(2040));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ROLE_EMPLOYEE",
                column: "OnCreated",
                value: new DateTime(2026, 1, 11, 19, 22, 4, 0, DateTimeKind.Utc).AddTicks(2073));
        }
    }
}
