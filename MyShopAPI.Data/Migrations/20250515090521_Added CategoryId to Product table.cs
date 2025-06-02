using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategoryIdtoProducttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_Category",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_Category",
                table: "ProductImages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "237905a7-e1a9-4420-86ba-176a4de3f023");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6efdd8d8-5e5c-4a4f-b650-6b211fd6d372");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15de94fd-efd0-4a65-b6d2-5ebb049b32ee", null, "Administrator", "ADMINISTRATOR" },
                    { "7854d384-402f-4f33-90ba-c358598a5bbc", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15de94fd-efd0-4a65-b6d2-5ebb049b32ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7854d384-402f-4f33-90ba-c358598a5bbc");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "ProductImages",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "237905a7-e1a9-4420-86ba-176a4de3f023", null, "customer", "CUSTOMER" },
                    { "6efdd8d8-5e5c-4a4f-b650-6b211fd6d372", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_Category",
                table: "ProductImages",
                column: "Category");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_Category",
                table: "ProductImages",
                column: "Category",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
