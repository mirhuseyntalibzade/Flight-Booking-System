using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedstripetobooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeSessionId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e4cf424-d1da-4eec-83b8-8d4b46106834", new DateTime(2025, 2, 22, 17, 46, 53, 860, DateTimeKind.Local).AddTicks(9167), "AQAAAAIAAYagAAAAEEiYoksF4y00sOeDgLpke06UujOBjLnsxwvJ1QZkruBT7GhwI0prGnHarmoHHi0b9g==", "fd0331f2-9edb-49c8-aefd-08b6ea28642b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeSessionId",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7350e584-6628-4c10-9978-6d9c77a251f3", new DateTime(2025, 2, 22, 16, 52, 24, 99, DateTimeKind.Local).AddTicks(3919), "AQAAAAIAAYagAAAAEGQsbgHHE3b9xUb2Cl8Z3DYeW5H35vD/yBDDbMlJhx/9VEGk9u9hNi5wfQTOGbE+gw==", "40c04069-2edc-48b4-bd2b-a67f15d6f2dc" });
        }
    }
}
