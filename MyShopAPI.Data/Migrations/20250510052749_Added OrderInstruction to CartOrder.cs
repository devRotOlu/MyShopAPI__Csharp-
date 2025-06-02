using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrderInstructiontoCartOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15132532-e862-4ce4-b759-f08fddf098c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41d5e9af-feaf-444c-a80e-c2c190c640e7");

            migrationBuilder.DropColumn(
                name: "AdditionalInformation",
                table: "DeliveryProfiles");

            migrationBuilder.AddColumn<string>(
                name: "OrderInstruction",
                table: "CartOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5e300cbf-e931-44d1-9c45-3b6f909871b2", null, "customer", "CUSTOMER" },
                    { "dde63f4e-4a7f-4919-a7c8-4b2ca84ae4b3", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e300cbf-e931-44d1-9c45-3b6f909871b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dde63f4e-4a7f-4919-a7c8-4b2ca84ae4b3");

            migrationBuilder.DropColumn(
                name: "OrderInstruction",
                table: "CartOrders");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInformation",
                table: "DeliveryProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15132532-e862-4ce4-b759-f08fddf098c7", null, "Administrator", "ADMINISTRATOR" },
                    { "41d5e9af-feaf-444c-a80e-c2c190c640e7", null, "customer", "CUSTOMER" }
                });
        }
    }
}
