using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguredforDBBContexttriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "914a5ee1-63f8-4661-902c-eb62326c8a80");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dceebba9-2e0a-479e-bc9c-470fdc48b9e6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15132532-e862-4ce4-b759-f08fddf098c7", null, "Administrator", "ADMINISTRATOR" },
                    { "41d5e9af-feaf-444c-a80e-c2c190c640e7", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15132532-e862-4ce4-b759-f08fddf098c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41d5e9af-feaf-444c-a80e-c2c190c640e7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "914a5ee1-63f8-4661-902c-eb62326c8a80", null, "Administrator", "ADMINISTRATOR" },
                    { "dceebba9-2e0a-479e-bc9c-470fdc48b9e6", null, "customer", "CUSTOMER" }
                });
        }
    }
}
