using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedblog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BackgroundImage",
                table: "Blogs",
                newName: "BackgroundImageURL");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f7dfcf7-dc59-47a4-8276-0d244214972e", new DateTime(2025, 4, 19, 11, 52, 42, 781, DateTimeKind.Local).AddTicks(1924), "AQAAAAIAAYagAAAAEGlxkEPh/PEkp4NEOoAwdSxSXagcrhoerVD3GDFahMP9UcxV5GWLXnbE+cxieHEObg==", "c7c31e23-8cb5-4d65-902c-c25e4a76d60e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BackgroundImageURL",
                table: "Blogs",
                newName: "BackgroundImage");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7cde1cf-1257-4ea0-ac36-6edb19295fd7", new DateTime(2025, 4, 19, 11, 27, 53, 599, DateTimeKind.Local).AddTicks(5539), "AQAAAAIAAYagAAAAEGA7AqIQW2r0I4FSC995Vy/5lx7OSIpfchFaNh9lN8fdmr0dHOx2CcL0J/oRCQOKnA==", "93d0825e-cec5-42cf-9a09-ebdef32d0dbd" });
        }
    }
}
