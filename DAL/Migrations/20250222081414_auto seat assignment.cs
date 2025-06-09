using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class autoseatassignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AutoAssign",
                table: "Seats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AutoAssign",
                table: "SeatClasses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "652392b4-2d5f-41bd-9f3a-08c99edf0d25", new DateTime(2025, 2, 22, 12, 14, 14, 277, DateTimeKind.Local).AddTicks(163), "AQAAAAIAAYagAAAAEKfqCNoXdghaw5tOkbhAYEJE9SM1Xi6HrpkJ9sgP1hXdX42QDnSEr5a7LsuEOBT4Kg==", "19498a55-5f67-4610-99c9-9b819dc28b97" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoAssign",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "AutoAssign",
                table: "SeatClasses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89af4450-0a5b-4872-81cf-1c74e113c4e0", new DateTime(2025, 2, 21, 22, 46, 19, 12, DateTimeKind.Local).AddTicks(1713), "AQAAAAIAAYagAAAAEN7Op6Hzh7rtAm1yGzIIYXqX8wf00zufC1Z3AJYgCJa21Tv3GEXrdDR9+OxBruKnsA==", "90a09b23-7097-4cd9-98f9-bb86e9ae01f3" });
        }
    }
}
