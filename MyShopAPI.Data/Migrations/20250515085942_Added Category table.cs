using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategorytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "094b5a26-b250-4870-ab5b-38e362e926b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc51e784-3192-40ee-822e-99ab82943c34");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "ProductImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_Category",
                table: "ProductImages");

            migrationBuilder.DropTable(
                name: "Categories");

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

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "094b5a26-b250-4870-ab5b-38e362e926b1", null, "Administrator", "ADMINISTRATOR" },
                    { "cc51e784-3192-40ee-822e-99ab82943c34", null, "customer", "CUSTOMER" }
                });
        }
    }
}
