using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class newcartmodification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75af8aae-b30e-41f7-9059-40e3ae3ff743");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7812eca-d219-43a4-9644-cbabb5436622");

            migrationBuilder.DropColumn(
                name: "IsPurchased",
                table: "Carts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8d9cfb9a-aa21-4c72-9fea-4ddbaf4bcdb3", null, "customer", "CUSTOMER" },
                    { "b161e1ea-6e2c-4d9e-b5cc-c53897712d9c", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d9cfb9a-aa21-4c72-9fea-4ddbaf4bcdb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b161e1ea-6e2c-4d9e-b5cc-c53897712d9c");

            migrationBuilder.AddColumn<int>(
                name: "IsPurchased",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "75af8aae-b30e-41f7-9059-40e3ae3ff743", null, "customer", "CUSTOMER" },
                    { "c7812eca-d219-43a4-9644-cbabb5436622", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
