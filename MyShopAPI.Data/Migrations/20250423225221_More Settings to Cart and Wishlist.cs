using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoreSettingstoCartandWishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "942ca2d1-9e09-493a-b335-3db6f7237633");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3b9afd5-60b0-499b-b166-ad2d6ff508f3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6efd6134-2ef2-40c2-8f70-7bb3a879ea87", null, "customer", "CUSTOMER" },
                    { "9732dbba-f622-4cda-9a12-328b81e70081", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "942ca2d1-9e09-493a-b335-3db6f7237633", null, "customer", "CUSTOMER" },
                    { "c3b9afd5-60b0-499b-b166-ad2d6ff508f3", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
