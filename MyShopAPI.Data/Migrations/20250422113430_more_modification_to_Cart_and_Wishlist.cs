using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class more_modification_to_Cart_and_Wishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartCustomerOrder_Carts_ItemsOrderedCustomerId_ItemsOrderedProductId",
                table: "CartCustomerOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartCustomerOrder",
                table: "CartCustomerOrder");

            migrationBuilder.DropIndex(
                name: "IX_CartCustomerOrder_ItemsOrderedCustomerId_ItemsOrderedProductId",
                table: "CartCustomerOrder");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37d6db1f-4358-4353-8881-c7af34710b04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f5c159d-53c7-4bcd-9e4f-1c9dab85f45d");

            migrationBuilder.AddColumn<int>(
                name: "ItemsOrderedId",
                table: "CartCustomerOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists",
                columns: new[] { "CustomerId", "ProductId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                columns: new[] { "CustomerId", "ProductId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartCustomerOrder",
                table: "CartCustomerOrder",
                columns: new[] { "OrdersId", "ItemsOrderedCustomerId", "ItemsOrderedProductId", "ItemsOrderedId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "48c03fff-3bfe-430b-9b21-4aee61ba6e10", null, "customer", "CUSTOMER" },
                    { "87da5f04-71a9-4033-a0ae-7cb225b5a91e", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartCustomerOrder_ItemsOrderedCustomerId_ItemsOrderedProductId_ItemsOrderedId",
                table: "CartCustomerOrder",
                columns: new[] { "ItemsOrderedCustomerId", "ItemsOrderedProductId", "ItemsOrderedId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CartCustomerOrder_Carts_ItemsOrderedCustomerId_ItemsOrderedProductId_ItemsOrderedId",
                table: "CartCustomerOrder",
                columns: new[] { "ItemsOrderedCustomerId", "ItemsOrderedProductId", "ItemsOrderedId" },
                principalTable: "Carts",
                principalColumns: new[] { "CustomerId", "ProductId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartCustomerOrder_Carts_ItemsOrderedCustomerId_ItemsOrderedProductId_ItemsOrderedId",
                table: "CartCustomerOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartCustomerOrder",
                table: "CartCustomerOrder");

            migrationBuilder.DropIndex(
                name: "IX_CartCustomerOrder_ItemsOrderedCustomerId_ItemsOrderedProductId_ItemsOrderedId",
                table: "CartCustomerOrder");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48c03fff-3bfe-430b-9b21-4aee61ba6e10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87da5f04-71a9-4033-a0ae-7cb225b5a91e");

            migrationBuilder.DropColumn(
                name: "ItemsOrderedId",
                table: "CartCustomerOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists",
                columns: new[] { "CustomerId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                columns: new[] { "CustomerId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartCustomerOrder",
                table: "CartCustomerOrder",
                columns: new[] { "OrdersId", "ItemsOrderedCustomerId", "ItemsOrderedProductId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37d6db1f-4358-4353-8881-c7af34710b04", null, "customer", "CUSTOMER" },
                    { "9f5c159d-53c7-4bcd-9e4f-1c9dab85f45d", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartCustomerOrder_ItemsOrderedCustomerId_ItemsOrderedProductId",
                table: "CartCustomerOrder",
                columns: new[] { "ItemsOrderedCustomerId", "ItemsOrderedProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CartCustomerOrder_Carts_ItemsOrderedCustomerId_ItemsOrderedProductId",
                table: "CartCustomerOrder",
                columns: new[] { "ItemsOrderedCustomerId", "ItemsOrderedProductId" },
                principalTable: "Carts",
                principalColumns: new[] { "CustomerId", "ProductId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
