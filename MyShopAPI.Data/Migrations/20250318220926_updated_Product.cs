using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class updated_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37be7522-e14a-46fe-b33a-b61e78379df6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82398f5b-528e-4dab-868a-1e8ee1a729be");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "647ff58e-afd8-47ac-9fb8-143a595b1e31", null, "customer", "CUSTOMER" },
                    { "7fcdd8f9-e9aa-4dd6-b011-9ebc0a624aa7", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "647ff58e-afd8-47ac-9fb8-143a595b1e31");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fcdd8f9-e9aa-4dd6-b011-9ebc0a624aa7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37be7522-e14a-46fe-b33a-b61e78379df6", null, "customer", "CUSTOMER" },
                    { "82398f5b-528e-4dab-868a-1e8ee1a729be", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
