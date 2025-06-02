using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedReviewDatetoProductReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fbe028a-d537-47d4-b4d7-4449fdfd5719");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b1852ee-a98c-4884-a9b8-3886ca18c01c");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReviewDate",
                table: "ProductReviews",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39c54f53-fcb9-4cbf-bfb2-6bd488a27b5a", null, "Administrator", "ADMINISTRATOR" },
                    { "90d0cd08-db7c-4c8c-b6a4-fc285baf7b4d", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39c54f53-fcb9-4cbf-bfb2-6bd488a27b5a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90d0cd08-db7c-4c8c-b6a4-fc285baf7b4d");

            migrationBuilder.DropColumn(
                name: "ReviewDate",
                table: "ProductReviews");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7fbe028a-d537-47d4-b4d7-4449fdfd5719", null, "Administrator", "ADMINISTRATOR" },
                    { "9b1852ee-a98c-4884-a9b8-3886ca18c01c", null, "customer", "CUSTOMER" }
                });
        }
    }
}
