using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class changedbookingmodelv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Bookings_BookingId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_BookingId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Seats");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7350e584-6628-4c10-9978-6d9c77a251f3", new DateTime(2025, 2, 22, 16, 52, 24, 99, DateTimeKind.Local).AddTicks(3919), "AQAAAAIAAYagAAAAEGQsbgHHE3b9xUb2Cl8Z3DYeW5H35vD/yBDDbMlJhx/9VEGk9u9hNi5wfQTOGbE+gw==", "40c04069-2edc-48b4-bd2b-a67f15d6f2dc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Seats",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3dd242d-272a-4a8f-80da-3a341e850966", new DateTime(2025, 2, 22, 15, 13, 59, 30, DateTimeKind.Local).AddTicks(9638), "AQAAAAIAAYagAAAAEPLeE3m6OPAafbNe7nEys7tdXv5Cjvi8/AM4MJfjy2wQ+CQqN86u/Nfq7/oi9z17kg==", "bf496f03-197e-4b8c-bfeb-b22147096a96" });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_BookingId",
                table: "Seats",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Bookings_BookingId",
                table: "Seats",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }
    }
}
