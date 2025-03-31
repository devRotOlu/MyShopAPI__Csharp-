using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class modified_Customer_DeliveryProfile_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeliveryProfiles_CustomerId",
                table: "DeliveryProfiles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97a8c0f0-332b-49c0-81f3-ac4bf6fabb20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce730078-1cb6-404f-9dd7-3d0fbaa89920");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "560e6c64-b5db-4b2a-971c-8c22b2860a92", null, "customer", "CUSTOMER" },
                    { "ce4c298d-c03b-4a1e-aa8f-d17a95441a93", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryProfiles_CustomerId",
                table: "DeliveryProfiles",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeliveryProfiles_CustomerId",
                table: "DeliveryProfiles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "560e6c64-b5db-4b2a-971c-8c22b2860a92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce4c298d-c03b-4a1e-aa8f-d17a95441a93");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "97a8c0f0-332b-49c0-81f3-ac4bf6fabb20", null, "Administrator", "ADMINISTRATOR" },
                    { "ce730078-1cb6-404f-9dd7-3d0fbaa89920", null, "customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryProfiles_CustomerId",
                table: "DeliveryProfiles",
                column: "CustomerId",
                unique: true);
        }
    }
}
