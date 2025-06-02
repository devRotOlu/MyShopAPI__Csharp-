using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class modified_Cart_Wishlist_settings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f197a2f-48cb-47eb-88b0-208c8129b483");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0d805b1-b3d9-4cb4-a918-9f85a5ad564a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28c3b1f3-9746-4be3-a884-dcefcf080563", null, "Administrator", "ADMINISTRATOR" },
                    { "d0e5b298-3cd2-4b77-a779-b1d2ed583cec", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28c3b1f3-9746-4be3-a884-dcefcf080563");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0e5b298-3cd2-4b77-a779-b1d2ed583cec");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f197a2f-48cb-47eb-88b0-208c8129b483", null, "Administrator", "ADMINISTRATOR" },
                    { "c0d805b1-b3d9-4cb4-a918-9f85a5ad564a", null, "customer", "CUSTOMER" }
                });
        }
    }
}
