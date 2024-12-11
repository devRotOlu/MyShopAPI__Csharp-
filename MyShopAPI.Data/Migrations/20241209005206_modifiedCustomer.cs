using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class modifiedCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5688bf21-c2ad-4de1-86f6-d1f42ced10ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4bebeb7-fcc7-49e2-bc6d-441f3ca662a7");

            migrationBuilder.AlterColumn<string>(
                name: "BillingAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37b8e5a5-a0d4-4249-932d-fb8881c4cf42", null, "Administrator", "ADMINISTRATOR" },
                    { "68e8a9e8-80b9-4141-b35f-1ae346d2205b", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37b8e5a5-a0d4-4249-932d-fb8881c4cf42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68e8a9e8-80b9-4141-b35f-1ae346d2205b");

            migrationBuilder.AlterColumn<string>(
                name: "BillingAddress",
                table: "AspNetUsers",
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
                    { "5688bf21-c2ad-4de1-86f6-d1f42ced10ab", null, "customer", "CUSTOMER" },
                    { "a4bebeb7-fcc7-49e2-bc6d-441f3ca662a7", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
