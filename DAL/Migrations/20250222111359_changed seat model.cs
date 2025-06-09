using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class changedseatmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Passengers_PassengerId",
                table: "Seats");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3dd242d-272a-4a8f-80da-3a341e850966", new DateTime(2025, 2, 22, 15, 13, 59, 30, DateTimeKind.Local).AddTicks(9638), "AQAAAAIAAYagAAAAEPLeE3m6OPAafbNe7nEys7tdXv5Cjvi8/AM4MJfjy2wQ+CQqN86u/Nfq7/oi9z17kg==", "bf496f03-197e-4b8c-bfeb-b22147096a96" });

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Passengers_PassengerId",
                table: "Seats",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Passengers_PassengerId",
                table: "Seats");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb96edb8-7d0d-43e5-92a9-6ea10e6257be", new DateTime(2025, 2, 22, 15, 6, 33, 47, DateTimeKind.Local).AddTicks(7681), "AQAAAAIAAYagAAAAEKR98Dhe+ClRwCs5Xb7/dfMw22w85Yad3oAk0oeZJmlpredugsofc1rQf9oMDULQyw==", "4c0c1a73-3337-4d6c-803f-de2b2d2c4234" });

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Passengers_PassengerId",
                table: "Seats",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
