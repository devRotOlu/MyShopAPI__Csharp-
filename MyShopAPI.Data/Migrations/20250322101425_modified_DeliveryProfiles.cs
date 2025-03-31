using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class modified_DeliveryProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Directions",
                table: "CustomersDetails");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "CustomersDetails");

            migrationBuilder.DropColumn(
                name: "LGA",
                table: "CustomersDetails");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "CustomersDetails");

            migrationBuilder.CreateTable(
                name: "DeliveryProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LGA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Directions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryProfiles_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a7e8ba7-7a59-458a-8d8f-5bd1d089c022", null, "customer", "CUSTOMER" },
                    { "64f2296e-b3c8-4e6c-86d6-1aae5fb00244", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryProfiles_CustomerId",
                table: "DeliveryProfiles",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryProfiles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a7e8ba7-7a59-458a-8d8f-5bd1d089c022");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64f2296e-b3c8-4e6c-86d6-1aae5fb00244");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInformation",
                table: "CustomersDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Directions",
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

            migrationBuilder.AddColumn<string>(
                name: "LGA",
                table: "CustomersDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "CustomersDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "496f0c9d-28ae-47f9-bfc1-1b2fa371a2f5", null, "Administrator", "ADMINISTRATOR" },
                    { "e0493aac-fb21-4141-9fb2-c1ef32e0862b", null, "customer", "CUSTOMER" }
                });
        }
    }
}
