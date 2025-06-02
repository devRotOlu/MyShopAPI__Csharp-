using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class modified_Cart_and_Wishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "501bf431-b55d-40d5-b8b3-1dcb9767494f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba55cd7b-f518-4015-ae97-cc58f73e77d1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37d6db1f-4358-4353-8881-c7af34710b04", null, "customer", "CUSTOMER" },
                    { "9f5c159d-53c7-4bcd-9e4f-1c9dab85f45d", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37d6db1f-4358-4353-8881-c7af34710b04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f5c159d-53c7-4bcd-9e4f-1c9dab85f45d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "501bf431-b55d-40d5-b8b3-1dcb9767494f", null, "Administrator", "ADMINISTRATOR" },
                    { "ba55cd7b-f518-4015-ae97-cc58f73e77d1", null, "customer", "CUSTOMER" }
                });
        }
    }
}
