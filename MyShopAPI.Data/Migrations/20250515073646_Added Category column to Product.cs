using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategorycolumntoProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e300cbf-e931-44d1-9c45-3b6f909871b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dde63f4e-4a7f-4919-a7c8-4b2ca84ae4b3");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5e300cbf-e931-44d1-9c45-3b6f909871b2", null, "customer", "CUSTOMER" },
                    { "dde63f4e-4a7f-4919-a7c8-4b2ca84ae4b3", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
