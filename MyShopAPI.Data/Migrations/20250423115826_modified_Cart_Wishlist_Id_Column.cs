using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class modified_Cart_Wishlist_Id_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "31bc91d7-a5a8-425e-a9f2-289bfa5f2bda", null, "Administrator", "ADMINISTRATOR" },
                    { "d138ab07-d497-4180-8826-14259d1ac515", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31bc91d7-a5a8-425e-a9f2-289bfa5f2bda");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d138ab07-d497-4180-8826-14259d1ac515");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28c3b1f3-9746-4be3-a884-dcefcf080563", null, "Administrator", "ADMINISTRATOR" },
                    { "d0e5b298-3cd2-4b77-a779-b1d2ed583cec", null, "customer", "CUSTOMER" }
                });
        }
    }
}
