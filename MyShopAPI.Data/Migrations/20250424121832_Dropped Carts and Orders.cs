using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class DroppedCartsandOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartCustomerOrder");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0004a7b1-37d5-41cd-8384-6ad3a0645cf1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a73e13aa-7ada-4bc3-a09e-cf9eeb1695e7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10eedc04-0453-492f-aad1-a6b7039599f9", null, "Administrator", "ADMINISTRATOR" },
                    { "80c6529b-39cb-47a3-bd84-40c3b6381ff1", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10eedc04-0453-492f-aad1-a6b7039599f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80c6529b-39cb-47a3-bd84-40c3b6381ff1");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPurchased = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.CustomerId, x.ProductId });
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
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliveryProfileId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryProfiles_DeliveryProfileId",
                        column: x => x.DeliveryProfileId,
                        principalTable: "DeliveryProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartCustomerOrder",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ItemsOrderedCustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemsOrderedProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartCustomerOrder", x => new { x.OrdersId, x.ItemsOrderedCustomerId, x.ItemsOrderedProductId });
                    table.ForeignKey(
                        name: "FK_CartCustomerOrder_Carts_ItemsOrderedCustomerId_ItemsOrderedProductId",
                        columns: x => new { x.ItemsOrderedCustomerId, x.ItemsOrderedProductId },
                        principalTable: "Carts",
                        principalColumns: new[] { "CustomerId", "ProductId" },
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
                    { "0004a7b1-37d5-41cd-8384-6ad3a0645cf1", null, "customer", "CUSTOMER" },
                    { "a73e13aa-7ada-4bc3-a09e-cf9eeb1695e7", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartCustomerOrder_ItemsOrderedCustomerId_ItemsOrderedProductId",
                table: "CartCustomerOrder",
                columns: new[] { "ItemsOrderedCustomerId", "ItemsOrderedProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryProfileId",
                table: "Orders",
                column: "DeliveryProfileId");
        }
    }
}
