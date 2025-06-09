using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class newpropforblog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDesc",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91dcfb09-825e-4630-9686-78bbb3b12a57", new DateTime(2025, 4, 19, 12, 2, 38, 757, DateTimeKind.Local).AddTicks(3775), "AQAAAAIAAYagAAAAEJXh4dS2in+ajJK0RWRpF/8E1qf2X9CTcOdPZy3joDSTzqTM3hiduhki0XdZXFFOlA==", "2c0e951a-7e2e-4b7b-9d67-24d1fe757c58" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDesc",
                table: "Blogs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716",
                columns: new[] { "ConcurrencyStamp", "DOB", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f7dfcf7-dc59-47a4-8276-0d244214972e", new DateTime(2025, 4, 19, 11, 52, 42, 781, DateTimeKind.Local).AddTicks(1924), "AQAAAAIAAYagAAAAEGlxkEPh/PEkp4NEOoAwdSxSXagcrhoerVD3GDFahMP9UcxV5GWLXnbE+cxieHEObg==", "c7c31e23-8cb5-4d65-902c-c25e4a76d60e" });
        }
    }
}
