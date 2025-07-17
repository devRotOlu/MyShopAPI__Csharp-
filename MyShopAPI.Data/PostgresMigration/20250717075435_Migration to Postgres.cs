using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShopAPI.Data.PostgresMigration
{
    /// <inheritdoc />
    public partial class MigrationtoPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "380530ad-47c0-4250-a5ae-fb7cf24f435a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afb08170-9688-4db3-abbc-feb9e423b0a7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "59116de7-c86d-448d-9abc-4a565113d347", null, "customer", "CUSTOMER" },
                    { "b0107862-d7e2-4523-bfe3-05231ca6e673", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59116de7-c86d-448d-9abc-4a565113d347");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0107862-d7e2-4523-bfe3-05231ca6e673");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "380530ad-47c0-4250-a5ae-fb7cf24f435a", null, "customer", "CUSTOMER" },
                    { "afb08170-9688-4db3-abbc-feb9e423b0a7", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
