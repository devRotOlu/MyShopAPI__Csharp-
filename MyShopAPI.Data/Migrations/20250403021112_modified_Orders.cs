using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class modified_Orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01e6a27a-8755-43c5-97fb-31aaae64d787");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12ed93ed-5bdd-424b-b502-367bd4625739");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1b4f6867-2d9f-4b08-b028-abf19ebc4ff2", null, "Administrator", "ADMINISTRATOR" },
                    { "ca025044-a52e-4cdf-b2e0-605e978bb161", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b4f6867-2d9f-4b08-b028-abf19ebc4ff2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca025044-a52e-4cdf-b2e0-605e978bb161");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01e6a27a-8755-43c5-97fb-31aaae64d787", null, "customer", "CUSTOMER" },
                    { "12ed93ed-5bdd-424b-b502-367bd4625739", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
