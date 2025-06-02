using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class DroppedCustomerOrderOrderedQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04a41312-dc4e-4cb0-8dde-142d9be988ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17076ebc-b175-4e36-8695-b0817df8bb7a");

            migrationBuilder.DropColumn(
                name: "OrderedQuantity",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7fbe028a-d537-47d4-b4d7-4449fdfd5719", null, "Administrator", "ADMINISTRATOR" },
                    { "9b1852ee-a98c-4884-a9b8-3886ca18c01c", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fbe028a-d537-47d4-b4d7-4449fdfd5719");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b1852ee-a98c-4884-a9b8-3886ca18c01c");

            migrationBuilder.AddColumn<int>(
                name: "OrderedQuantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04a41312-dc4e-4cb0-8dde-142d9be988ee", null, "Administrator", "ADMINISTRATOR" },
                    { "17076ebc-b175-4e36-8695-b0817df8bb7a", null, "customer", "CUSTOMER" }
                });
        }
    }
}
