using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCartOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsPurchased = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
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
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    OrderedQuantity = table.Column<int>(type: "int", nullable: false),
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
                name: "CartOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderedQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartOrders", x => new { x.OrderId, x.ProductId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_CartOrders_Carts_CustomerId_ProductId",
                        columns: x => new { x.CustomerId, x.ProductId },
                        principalTable: "Carts",
                        principalColumns: new[] { "CustomerId", "ProductId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04a41312-dc4e-4cb0-8dde-142d9be988ee", null, "Administrator", "ADMINISTRATOR" },
                    { "17076ebc-b175-4e36-8695-b0817df8bb7a", null, "customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartOrders_CustomerId_ProductId",
                table: "CartOrders",
                columns: new[] { "CustomerId", "ProductId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartOrders");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04a41312-dc4e-4cb0-8dde-142d9be988ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17076ebc-b175-4e36-8695-b0817df8bb7a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10eedc04-0453-492f-aad1-a6b7039599f9", null, "Administrator", "ADMINISTRATOR" },
                    { "80c6529b-39cb-47a3-bd84-40c3b6381ff1", null, "customer", "CUSTOMER" }
                });
        }
    }
}
