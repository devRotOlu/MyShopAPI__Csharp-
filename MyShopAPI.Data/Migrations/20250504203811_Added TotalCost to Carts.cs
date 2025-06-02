using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTotalCosttoCarts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39c54f53-fcb9-4cbf-bfb2-6bd488a27b5a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90d0cd08-db7c-4c8c-b6a4-fc285baf7b4d");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "Carts",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "914a5ee1-63f8-4661-902c-eb62326c8a80", null, "Administrator", "ADMINISTRATOR" },
                    { "dceebba9-2e0a-479e-bc9c-470fdc48b9e6", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "914a5ee1-63f8-4661-902c-eb62326c8a80");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dceebba9-2e0a-479e-bc9c-470fdc48b9e6");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "Carts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39c54f53-fcb9-4cbf-bfb2-6bd488a27b5a", null, "Administrator", "ADMINISTRATOR" },
                    { "90d0cd08-db7c-4c8c-b6a4-fc285baf7b4d", null, "customer", "CUSTOMER" }
                });
        }
    }
}
