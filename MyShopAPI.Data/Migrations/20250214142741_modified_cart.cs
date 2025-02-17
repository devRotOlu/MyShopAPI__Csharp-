using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class modified_cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ac60468-4ab9-4971-9ae3-cf0c67b1269c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d655fb0d-3022-462a-9d1b-0bc5ae8fc348");

            migrationBuilder.AddColumn<int>(
                name: "IsPurchased",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f913966-7e70-44c5-90d1-6b36ce5a320d", null, "Administrator", "ADMINISTRATOR" },
                    { "3668b7c1-a847-40c0-9cbf-50a82de62331", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f913966-7e70-44c5-90d1-6b36ce5a320d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3668b7c1-a847-40c0-9cbf-50a82de62331");

            migrationBuilder.DropColumn(
                name: "IsPurchased",
                table: "Carts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ac60468-4ab9-4971-9ae3-cf0c67b1269c", null, "customer", "CUSTOMER" },
                    { "d655fb0d-3022-462a-9d1b-0bc5ae8fc348", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
