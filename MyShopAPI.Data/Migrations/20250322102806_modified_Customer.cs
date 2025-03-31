using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class modified_Customer : Migration
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
                keyValue: "1a7e8ba7-7a59-458a-8d8f-5bd1d089c022");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64f2296e-b3c8-4e6c-86d6-1aae5fb00244");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "1a7e8ba7-7a59-458a-8d8f-5bd1d089c022", null, "customer", "CUSTOMER" },
                    { "64f2296e-b3c8-4e6c-86d6-1aae5fb00244", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryProfiles_CustomerId",
                table: "DeliveryProfiles",
                column: "CustomerId");
        }
    }
}
