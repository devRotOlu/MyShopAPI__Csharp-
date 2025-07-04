using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsDefaultProfiletoDeliveryProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "018376e0-8c76-4afb-8eaa-fec4e3fa9ddc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f6a18e8-6e24-49df-ae4e-11cd02f5a048");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultProfile",
                table: "DeliveryProfiles",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "298d4eba-02bd-4a71-be3c-3e59ef7440ca", null, "Administrator", "ADMINISTRATOR" },
                    { "aa6bb1af-8b2e-404f-9be3-e84758142a6c", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "298d4eba-02bd-4a71-be3c-3e59ef7440ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa6bb1af-8b2e-404f-9be3-e84758142a6c");

            migrationBuilder.DropColumn(
                name: "IsDefaultProfile",
                table: "DeliveryProfiles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "018376e0-8c76-4afb-8eaa-fec4e3fa9ddc", null, "customer", "CUSTOMER" },
                    { "6f6a18e8-6e24-49df-ae4e-11cd02f5a048", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
