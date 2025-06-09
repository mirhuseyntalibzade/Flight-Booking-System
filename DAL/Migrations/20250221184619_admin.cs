using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "897883a6-438e-4710-8224-0066485fa2b7", null, "Admin", "ADMİN" },
                    { "b4e2b8fd-5b95-4679-ac72-dc6db51257f8", null, "Manager", "MANAGER" },
                    { "b67f8d17-ca53-4b68-bdaa-67c965d09308", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DOB", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "458b8206-7801-4e07-b9d7-1567e5adc716", 0, "89af4450-0a5b-4872-81cf-1c74e113c4e0", new DateTime(2025, 2, 21, 22, 46, 19, 12, DateTimeKind.Local).AddTicks(1713), "mirhuseyntalibzade2004@gmail.com", false, "Mirhuseyn", 0, "Talibzade", false, null, "MIRHUSEYNTALIBZADE2004@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEN7Op6Hzh7rtAm1yGzIIYXqX8wf00zufC1Z3AJYgCJa21Tv3GEXrdDR9+OxBruKnsA==", null, false, "90a09b23-7097-4cd9-98f9-bb86e9ae01f3", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "897883a6-438e-4710-8224-0066485fa2b7", "458b8206-7801-4e07-b9d7-1567e5adc716" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4e2b8fd-5b95-4679-ac72-dc6db51257f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b67f8d17-ca53-4b68-bdaa-67c965d09308");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "897883a6-438e-4710-8224-0066485fa2b7", "458b8206-7801-4e07-b9d7-1567e5adc716" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "897883a6-438e-4710-8224-0066485fa2b7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "458b8206-7801-4e07-b9d7-1567e5adc716");
        }
    }
}
