using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.PostgresMigration
{
    /// <inheritdoc />
    public partial class droppedOrderIdfromProductReview : Migration
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
                keyValue: "59116de7-c86d-448d-9abc-4a565113d347");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0107862-d7e2-4523-bfe3-05231ca6e673");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ProductReviews");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductReviews",
                table: "ProductReviews",
                columns: new[] { "ReviewerId", "ProductId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bc9c358d-abb0-47be-943d-3b7ff0072f89", null, "customer", "CUSTOMER" },
                    { "f847c623-0846-4481-aff5-c9e6879c7f8f", null, "Administrator", "ADMINISTRATOR" }
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
                keyValue: "bc9c358d-abb0-47be-943d-3b7ff0072f89");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f847c623-0846-4481-aff5-c9e6879c7f8f");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ProductReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductReviews",
                table: "ProductReviews",
                columns: new[] { "ReviewerId", "ProductId", "OrderId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "59116de7-c86d-448d-9abc-4a565113d347", null, "customer", "CUSTOMER" },
                    { "b0107862-d7e2-4523-bfe3-05231ca6e673", null, "Administrator", "ADMINISTRATOR" }
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
