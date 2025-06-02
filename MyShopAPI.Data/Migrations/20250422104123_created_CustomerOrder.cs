using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class created_CustomerOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DeliveryProfileId = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    { "501bf431-b55d-40d5-b8b3-1dcb9767494f", null, "Administrator", "ADMINISTRATOR" },
                    { "ba55cd7b-f518-4015-ae97-cc58f73e77d1", null, "customer", "CUSTOMER" }
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartCustomerOrder");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "501bf431-b55d-40d5-b8b3-1dcb9767494f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba55cd7b-f518-4015-ae97-cc58f73e77d1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "666a200a-4709-4f32-9871-37f0a892ffd7", null, "Administrator", "ADMINISTRATOR" },
                    { "723052c9-5ede-4b17-a0e8-a5aa08c2f888", null, "customer", "CUSTOMER" }
                });
        }
    }
}
