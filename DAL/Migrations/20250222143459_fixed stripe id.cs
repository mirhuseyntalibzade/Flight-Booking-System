using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixedstripeid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StripeSessionId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7cd013f-9332-4da0-ac56-792e526d0f46", new DateTime(2025, 2, 22, 18, 34, 58, 725, DateTimeKind.Local).AddTicks(3624), "AQAAAAIAAYagAAAAED6nETx0uPGia5C2hkEVh4d10HukyVzc28y6QmETomTki+9TUK8vyghk75hahkFARw==", "14faa0ef-fa9e-4086-854f-7da2e41e598b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StripeSessionId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e4cf424-d1da-4eec-83b8-8d4b46106834", new DateTime(2025, 2, 22, 17, 46, 53, 860, DateTimeKind.Local).AddTicks(9167), "AQAAAAIAAYagAAAAEEiYoksF4y00sOeDgLpke06UujOBjLnsxwvJ1QZkruBT7GhwI0prGnHarmoHHi0b9g==", "fd0331f2-9edb-49c8-aefd-08b6ea28642b" });
        }
    }
}
