using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ceb28d89-d72c-48f9-a1d7-e666f5789368");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d644a522-cf1d-4a3c-a0a2-4dbed7c1e283");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6ab9eedb-6c3a-45cb-8bbd-00fadf62d9bf", null, "customer", "CUSTOMER" },
                    { "d9380c1b-f9f9-4eb5-814d-b4c2ed0e8a5f", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ab9eedb-6c3a-45cb-8bbd-00fadf62d9bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9380c1b-f9f9-4eb5-814d-b4c2ed0e8a5f");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ceb28d89-d72c-48f9-a1d7-e666f5789368", null, "Administrator", "ADMINISTRATOR" },
                    { "d644a522-cf1d-4a3c-a0a2-4dbed7c1e283", null, "customer", "CUSTOMER" }
                });
        }
    }
}
