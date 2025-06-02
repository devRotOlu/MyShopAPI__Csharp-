using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class modifiedcartandwishlistconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "942ca2d1-9e09-493a-b335-3db6f7237633", null, "customer", "CUSTOMER" },
                    { "c3b9afd5-60b0-499b-b166-ad2d6ff508f3", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "31bc91d7-a5a8-425e-a9f2-289bfa5f2bda", null, "Administrator", "ADMINISTRATOR" },
                    { "d138ab07-d497-4180-8826-14259d1ac515", null, "customer", "CUSTOMER" }
                });
        }
    }
}
