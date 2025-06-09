using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedrowsandcolumnstoseattable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Column",
                table: "Seats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8df4c496-f91a-4d3e-8272-6a2a475aaca9", new DateTime(2025, 2, 22, 13, 3, 14, 693, DateTimeKind.Local).AddTicks(1402), "AQAAAAIAAYagAAAAEBVa7x9kl6bty3ya4q3M59OE/YW6bb39DQoKoIZSio8xX5L2aOZAnwitRF54LyUL9w==", "82f37f31-0fd2-42ce-8190-58dc629a71c6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Column",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "Row",
                table: "Seats");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "652392b4-2d5f-41bd-9f3a-08c99edf0d25", new DateTime(2025, 2, 22, 12, 14, 14, 277, DateTimeKind.Local).AddTicks(163), "AQAAAAIAAYagAAAAEKfqCNoXdghaw5tOkbhAYEJE9SM1Xi6HrpkJ9sgP1hXdX42QDnSEr5a7LsuEOBT4Kg==", "19498a55-5f67-4610-99c9-9b819dc28b97" });
        }
    }
}
