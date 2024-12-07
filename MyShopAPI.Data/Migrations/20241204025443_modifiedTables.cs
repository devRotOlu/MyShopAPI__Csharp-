using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class modifiedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_AspNetUsers_CustomerId",
                table: "ProductReviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab7e8402-4085-411c-ae61-37de53325964");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "daa1d784-17ba-431f-9aa3-3e14fd77cee8");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "ProductReviews",
                newName: "ReviewerId");

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "Products",
                type: "decimal(3,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePublicId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrI",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0658094c-1ffd-449a-bf56-45e5e003c018", null, "Administrator", "ADMINISTRATOR" },
                    { "3b16c426-858e-4514-8f65-3dc29c3c9b2f", null, "customer", "CUSTOMER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_AspNetUsers_ReviewerId",
                table: "ProductReviews",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_AspNetUsers_ReviewerId",
                table: "ProductReviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0658094c-1ffd-449a-bf56-45e5e003c018");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b16c426-858e-4514-8f65-3dc29c3c9b2f");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProfilePicturePublicId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrI",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ReviewerId",
                table: "ProductReviews",
                newName: "CustomerId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ab7e8402-4085-411c-ae61-37de53325964", null, "Administrator", "ADMINISTRATOR" },
                    { "daa1d784-17ba-431f-9aa3-3e14fd77cee8", null, "customer", "CUSTOMER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_AspNetUsers_CustomerId",
                table: "ProductReviews",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
