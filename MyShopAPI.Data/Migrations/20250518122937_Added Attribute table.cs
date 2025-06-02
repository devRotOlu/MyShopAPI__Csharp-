using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAttributetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d2ddcfe-9416-4195-a6ea-8cad639bfff7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47dd3934-c5a1-49fe-83b4-b177d02046b4");

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    AttributeId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => new { x.AttributeId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ceb28d89-d72c-48f9-a1d7-e666f5789368", null, "Administrator", "ADMINISTRATOR" },
                    { "d644a522-cf1d-4a3c-a0a2-4dbed7c1e283", null, "customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductId",
                table: "ProductAttributes",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ceb28d89-d72c-48f9-a1d7-e666f5789368");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d644a522-cf1d-4a3c-a0a2-4dbed7c1e283");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d2ddcfe-9416-4195-a6ea-8cad639bfff7", null, "customer", "CUSTOMER" },
                    { "47dd3934-c5a1-49fe-83b4-b177d02046b4", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
