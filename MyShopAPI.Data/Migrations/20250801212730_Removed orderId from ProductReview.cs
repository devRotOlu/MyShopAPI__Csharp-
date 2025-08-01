using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedorderIdfromProductReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_Orders_OrderId",
                table: "ProductReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductReviews",
                table: "ProductReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_OrderId",
                table: "ProductReviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc70a1b9-d2b4-4133-a94f-bd6ec731e54a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4140dae-5776-4435-9019-183350936d93");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ProductReviews");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "Products",
                type: "decimal(13,2)",
                precision: 13,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReviewDate",
                table: "ProductReviews",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductReviews",
                table: "ProductReviews",
                columns: new[] { "ReviewerId", "ProductId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05aed6a1-5eed-4990-bb54-aa6f4db43d33", null, "Administrator", "ADMINISTRATOR" },
                    { "233cd0a2-c6fc-411e-aab6-b7711344c304", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductReviews",
                table: "ProductReviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05aed6a1-5eed-4990-bb54-aa6f4db43d33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "233cd0a2-c6fc-411e-aab6-b7711344c304");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "Products",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(13,2)",
                oldPrecision: 13,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReviewDate",
                table: "ProductReviews",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ProductReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "OrderDate",
                table: "Orders",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductReviews",
                table: "ProductReviews",
                columns: new[] { "ReviewerId", "ProductId", "OrderId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "cc70a1b9-d2b4-4133-a94f-bd6ec731e54a", null, "customer", "CUSTOMER" },
                    { "d4140dae-5776-4435-9019-183350936d93", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_OrderId",
                table: "ProductReviews",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_Orders_OrderId",
                table: "ProductReviews",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
