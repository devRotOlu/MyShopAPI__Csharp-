using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedCartandWishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ab9eedb-6c3a-45cb-8bbd-00fadf62d9bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9380c1b-f9f9-4eb5-814d-b4c2ed0e8a5f");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedAt",
                table: "Wishlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Wishlists",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Wishlists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedAt",
                table: "Carts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "018376e0-8c76-4afb-8eaa-fec4e3fa9ddc", null, "customer", "CUSTOMER" },
                    { "6f6a18e8-6e24-49df-ae4e-11cd02f5a048", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "018376e0-8c76-4afb-8eaa-fec4e3fa9ddc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f6a18e8-6e24-49df-ae4e-11cd02f5a048");

            migrationBuilder.DropColumn(
                name: "AddedAt",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "AddedAt",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Carts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6ab9eedb-6c3a-45cb-8bbd-00fadf62d9bf", null, "customer", "CUSTOMER" },
                    { "d9380c1b-f9f9-4eb5-814d-b4c2ed0e8a5f", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
