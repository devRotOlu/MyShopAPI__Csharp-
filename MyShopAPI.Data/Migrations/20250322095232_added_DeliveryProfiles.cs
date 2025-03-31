using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class added_DeliveryProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "647ff58e-afd8-47ac-9fb8-143a595b1e31");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fcdd8f9-e9aa-4dd6-b011-9ebc0a624aa7");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress",
                table: "CustomersDetails",
                newName: "StreetAddress");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureUrI",
                table: "CustomersDetails",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "ProfilePicturePublicId",
                table: "CustomersDetails",
                newName: "LGA");

            migrationBuilder.RenameColumn(
                name: "BillingAddress",
                table: "CustomersDetails",
                newName: "Directions");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "CustomersDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInformation",
                table: "CustomersDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CustomersDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "CustomersDetails",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "496f0c9d-28ae-47f9-bfc1-1b2fa371a2f5", null, "Administrator", "ADMINISTRATOR" },
                    { "e0493aac-fb21-4141-9fb2-c1ef32e0862b", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "496f0c9d-28ae-47f9-bfc1-1b2fa371a2f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0493aac-fb21-4141-9fb2-c1ef32e0862b");

            migrationBuilder.DropColumn(
                name: "AdditionalInformation",
                table: "CustomersDetails");

            migrationBuilder.DropColumn(
                name: "City",
                table: "CustomersDetails");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "CustomersDetails");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "CustomersDetails",
                newName: "ShippingAddress");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "CustomersDetails",
                newName: "ProfilePictureUrI");

            migrationBuilder.RenameColumn(
                name: "LGA",
                table: "CustomersDetails",
                newName: "ProfilePicturePublicId");

            migrationBuilder.RenameColumn(
                name: "Directions",
                table: "CustomersDetails",
                newName: "BillingAddress");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "CustomersDetails",
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
                    { "647ff58e-afd8-47ac-9fb8-143a595b1e31", null, "customer", "CUSTOMER" },
                    { "7fcdd8f9-e9aa-4dd6-b011-9ebc0a624aa7", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
