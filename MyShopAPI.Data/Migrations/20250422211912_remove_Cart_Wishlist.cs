using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class remove_Cart_Wishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartCustomerOrder");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48c03fff-3bfe-430b-9b21-4aee61ba6e10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87da5f04-71a9-4033-a0ae-7cb225b5a91e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "58930887-a0dc-4378-ab3a-a96c9654c537", null, "customer", "CUSTOMER" },
                    { "9349049e-167d-4be7-91e6-3cb2546e0e72", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58930887-a0dc-4378-ab3a-a96c9654c537");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9349049e-167d-4be7-91e6-3cb2546e0e72");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsPurchased = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.CustomerId, x.ProductId, x.Id });
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => new { x.CustomerId, x.ProductId, x.Id });
                    table.ForeignKey(
                        name: "FK_Wishlists_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wishlists_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartCustomerOrder",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ItemsOrderedCustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemsOrderedProductId = table.Column<int>(type: "int", nullable: false),
                    ItemsOrderedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartCustomerOrder", x => new { x.OrdersId, x.ItemsOrderedCustomerId, x.ItemsOrderedProductId, x.ItemsOrderedId });
                    table.ForeignKey(
                        name: "FK_CartCustomerOrder_Carts_ItemsOrderedCustomerId_ItemsOrderedProductId_ItemsOrderedId",
                        columns: x => new { x.ItemsOrderedCustomerId, x.ItemsOrderedProductId, x.ItemsOrderedId },
                        principalTable: "Carts",
                        principalColumns: new[] { "CustomerId", "ProductId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartCustomerOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ProductId",
                table: "Wishlists",
                column: "ProductId");
        }
    }
}
