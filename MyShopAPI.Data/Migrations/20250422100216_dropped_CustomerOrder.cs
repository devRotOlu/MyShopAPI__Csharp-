using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class dropped_CustomerOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartCustomerOrder");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b4f6867-2d9f-4b08-b028-abf19ebc4ff2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca025044-a52e-4cdf-b2e0-605e978bb161");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "666a200a-4709-4f32-9871-37f0a892ffd7", null, "Administrator", "ADMINISTRATOR" },
                    { "723052c9-5ede-4b17-a0e8-a5aa08c2f888", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "666a200a-4709-4f32-9871-37f0a892ffd7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "723052c9-5ede-4b17-a0e8-a5aa08c2f888");

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
                    { "1b4f6867-2d9f-4b08-b028-abf19ebc4ff2", null, "Administrator", "ADMINISTRATOR" },
                    { "ca025044-a52e-4cdf-b2e0-605e978bb161", null, "customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartCustomerOrder_ItemsOrderedCustomerId_ItemsOrderedProductId",
                table: "CartCustomerOrder",
                columns: new[] { "ItemsOrderedCustomerId", "ItemsOrderedProductId" });

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
