using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixedtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Flights_FlightId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_FlightId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ffa419bd-7a3d-416f-b3b8-55ba5315c570", new DateTime(2025, 2, 23, 13, 42, 8, 284, DateTimeKind.Local).AddTicks(4195), "AQAAAAIAAYagAAAAENW9jdnjnrZITg56sgWYhewwNayDyQA8mQD3Zz0eMXCPQD4qqKDWchB1cSR7ZZvDCA==", "4a8e3fac-b18f-49a8-bc50-0ec15a63ba11" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2e89b07-6508-422e-af96-bcfb73ea4541", new DateTime(2025, 2, 23, 12, 44, 9, 934, DateTimeKind.Local).AddTicks(2388), "AQAAAAIAAYagAAAAEMQ/sJwE5DKNdlDem40AqcXTHbEod/XG5yuS5cePYI0jGry94ytvevV5fSdz1dpIPQ==", "9c4f0843-0124-4e30-9341-49f5f46c2664" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightId",
                table: "Bookings",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Flights_FlightId",
                table: "Bookings",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
