using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Moreconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6efd6134-2ef2-40c2-8f70-7bb3a879ea87");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9732dbba-f622-4cda-9a12-328b81e70081");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0004a7b1-37d5-41cd-8384-6ad3a0645cf1", null, "customer", "CUSTOMER" },
                    { "a73e13aa-7ada-4bc3-a09e-cf9eeb1695e7", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0004a7b1-37d5-41cd-8384-6ad3a0645cf1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a73e13aa-7ada-4bc3-a09e-cf9eeb1695e7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6efd6134-2ef2-40c2-8f70-7bb3a879ea87", null, "customer", "CUSTOMER" },
                    { "9732dbba-f622-4cda-9a12-328b81e70081", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
