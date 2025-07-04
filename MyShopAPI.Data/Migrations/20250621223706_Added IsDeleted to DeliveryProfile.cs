using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsDeletedtoDeliveryProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "298d4eba-02bd-4a71-be3c-3e59ef7440ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa6bb1af-8b2e-404f-9be3-e84758142a6c");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DeliveryProfiles",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "91ac2bc4-a711-4b16-aeca-d861c17ece32", null, "Administrator", "ADMINISTRATOR" },
                    { "f4afc9ec-ed02-48bf-a79f-db2204586b67", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91ac2bc4-a711-4b16-aeca-d861c17ece32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4afc9ec-ed02-48bf-a79f-db2204586b67");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DeliveryProfiles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "298d4eba-02bd-4a71-be3c-3e59ef7440ca", null, "Administrator", "ADMINISTRATOR" },
                    { "aa6bb1af-8b2e-404f-9be3-e84758142a6c", null, "customer", "CUSTOMER" }
                });
        }
    }
}
