using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class m2mflightbooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingFlight",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingFlight", x => new { x.BookingId, x.FlightId });
                    table.ForeignKey(
                        name: "FK_BookingFlight_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingFlight_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2e89b07-6508-422e-af96-bcfb73ea4541", new DateTime(2025, 2, 23, 12, 44, 9, 934, DateTimeKind.Local).AddTicks(2388), "AQAAAAIAAYagAAAAEMQ/sJwE5DKNdlDem40AqcXTHbEod/XG5yuS5cePYI0jGry94ytvevV5fSdz1dpIPQ==", "9c4f0843-0124-4e30-9341-49f5f46c2664" });

            migrationBuilder.CreateIndex(
                name: "IX_BookingFlight_FlightId",
                table: "BookingFlight",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingFlight");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7cd013f-9332-4da0-ac56-792e526d0f46", new DateTime(2025, 2, 22, 18, 34, 58, 725, DateTimeKind.Local).AddTicks(3624), "AQAAAAIAAYagAAAAED6nETx0uPGia5C2hkEVh4d10HukyVzc28y6QmETomTki+9TUK8vyghk75hahkFARw==", "14faa0ef-fa9e-4086-854f-7da2e41e598b" });
        }
    }
}
